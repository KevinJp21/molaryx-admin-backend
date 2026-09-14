# Restaurar la BD en el VPS tras un bug en producción

Si el **pipeline hace rollback durante el deploy** (migración fallida o health check fallido), la BD se restaura sola: detiene la API, recrea la base (`DROP` + `CREATE`) y aplica el backup custom con `pg_restore`.

Si el **deploy terminó OK** y después encuentras un bug, la restauración de PostgreSQL es **manual**. Reejecutar el tag anterior en GitHub solo vuelve a desplegar el código/contenedor; **no restaura la BD**.

## Convención de backups

Tras cada deploy a un tag nuevo (ej. `v0.1.15`), quedan archivos como:

```text
/opt/molaryx/backups/pre-v0.1.15-20260824153000.dump
/opt/molaryx/backups/pre-v0.1.15-20260824153000.dump.tag
```

| Archivo | Contenido |
| --- | --- |
| `.dump` | Dump custom (`pg_dump -Fc`) de la BD **justo antes** de desplegar el tag nuevo |
| `.tag` | Tag de la API que estaba corriendo cuando se tomó ese backup (ej. `v0.1.14`) |

## Por qué recrear la BD en el rollback

Restaurar un dump sobre una BD que ya tiene objetos (con `--clean`) falla con dependencias (PK/FK). Recrear la BD garantiza un estado vacío y el restore reconstruye todo el snapshot del backup.

**Importante:** esto **solo** ocurre en rollback automático o restauración manual. El deploy normal sigue siendo: backup → pull → migrate → start → health. Nunca hace `DROP DATABASE` en un deploy exitoso.

## Antes del rollback manual: no olvides esto

El archivo `pre-<tag>-….dump` es un snapshot de la BD **antes del despliegue de ese tag**.

Si restauras `pre-v0.1.15-….dump`:

- la BD vuelve al estado **previo** a desplegar `v0.1.15` (compatible con la API del `.tag`, p. ej. `v0.1.14`);
- **se pierden** migraciones, datos y cambios hechos **después** de ese deploy (citas, pacientes, pagos, etc. creados ya en `v0.1.15`).

No es un “undo” parcial: es volver exactamente a cómo estaba la BD en el momento del backup, justo antes del último tag.

## Flujo completo (ejemplo)

Estado actual:

- API → `v0.1.15`
- BD → esquema/datos de `v0.1.15`
- Bug descubierto **después** de un deploy exitoso

### 1. Detener la API

```bash
cd /opt/molaryx
docker compose stop api
```

### 2. Recrear la BD y restaurar el backup

```bash
POSTGRES_USER=$(grep '^POSTGRES_USER=' .env | cut -d '=' -f2-)
POSTGRES_DB=$(grep '^POSTGRES_DB=' .env | cut -d '=' -f2-)
BACKUP=/opt/molaryx/backups/pre-v0.1.15-20260824153000.dump

docker compose exec -T postgres \
  psql \
  -U "$POSTGRES_USER" \
  -d postgres \
  -v ON_ERROR_STOP=1 \
  -c "DROP DATABASE \"$POSTGRES_DB\" WITH (FORCE);"

docker compose exec -T postgres \
  psql \
  -U "$POSTGRES_USER" \
  -d postgres \
  -v ON_ERROR_STOP=1 \
  -c "CREATE DATABASE \"$POSTGRES_DB\";"

docker compose exec -T postgres \
  pg_restore \
  -U "$POSTGRES_USER" \
  -d "$POSTGRES_DB" \
  --clean \
  --if-exists \
  --no-owner \
  < "$BACKUP"
```

Sustituye la ruta del `.dump` por el backup que corresponda al deploy que quieres revertir.

### 3. Volver a la versión de API del `.tag`

```bash
cat /opt/molaryx/backups/pre-v0.1.15-20260824153000.dump.tag
# ejemplo: v0.1.14

PREVIOUS_TAG=$(cat /opt/molaryx/backups/pre-v0.1.15-20260824153000.dump.tag)

sed -i "s/^IMAGE_TAG=.*/IMAGE_TAG=${PREVIOUS_TAG}/" .env

IMAGE_TAG="${PREVIOUS_TAG}" docker compose pull api
docker compose up -d api
```

### 4. Verificar

```bash
docker compose ps
curl -f http://127.0.0.1:3000/
docker logs molaryx-api --tail 100
```

## Script compacto

```bash
cd /opt/molaryx

BACKUP=/opt/molaryx/backups/pre-v0.1.15-20260824153000.dump

docker compose stop api

POSTGRES_USER=$(grep '^POSTGRES_USER=' .env | cut -d '=' -f2-)
POSTGRES_DB=$(grep '^POSTGRES_DB=' .env | cut -d '=' -f2-)

docker compose exec -T postgres \
  psql \
  -U "$POSTGRES_USER" \
  -d postgres \
  -v ON_ERROR_STOP=1 \
  -c "DROP DATABASE \"$POSTGRES_DB\" WITH (FORCE);"

docker compose exec -T postgres \
  psql \
  -U "$POSTGRES_USER" \
  -d postgres \
  -v ON_ERROR_STOP=1 \
  -c "CREATE DATABASE \"$POSTGRES_DB\";"

docker compose exec -T postgres \
  pg_restore \
  -U "$POSTGRES_USER" \
  -d "$POSTGRES_DB" \
  --clean \
  --if-exists \
  --no-owner \
  < "$BACKUP"

PREVIOUS_TAG=$(cat "${BACKUP}.tag")

sed -i "s/^IMAGE_TAG=.*/IMAGE_TAG=${PREVIOUS_TAG}/" .env

IMAGE_TAG="${PREVIOUS_TAG}" docker compose pull api
docker compose up -d api

docker compose ps
curl -f http://127.0.0.1:3000/
```

## Resumen

| Qué quieres revertir | Cómo |
| --- | --- |
| Código / contenedor | GitHub → reejecutar (o desplegar) el tag anterior |
| Estado de PostgreSQL | Recrear BD + `pg_restore` del `.dump` correspondiente |
| Qué API va con ese dump | Leer el `.tag` junto al backup |

**No** ejecutes el tag anterior esperando que eso restaure la BD. Son dos operaciones distintas: código vía deploy/tag, datos vía backup.

## Nota

Esto asume que vuelves a la pareja **API del `.tag` + BD del `.dump`**. Tras restaurar el dump, no vuelvas a levantar la versión nueva sin migrar de nuevo: esquema y código deben quedar alineados.

Los backups antiguos en `.sql` (plano) se restauran con `psql`, no con `pg_restore`. A partir de este cambio, los nuevos son `.dump` (custom).

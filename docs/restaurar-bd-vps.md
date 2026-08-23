# Restaurar la BD en el VPS tras un bug en producción

Si el **pipeline hace rollback durante el deploy** (migración fallida o health check fallido), la BD se restaura sola.

Si el **deploy terminó OK** y después encuentras un bug, la restauración de PostgreSQL es **manual**. Reejecutar el tag anterior en GitHub solo vuelve a desplegar el código/contenedor; **no restaura la BD**.

## Convención de backups

Tras cada deploy a un tag nuevo (ej. `v0.1.14`), quedan archivos como:

```text
/opt/molaryx/backups/pre-v0.1.14-20260822153000.sql
/opt/molaryx/backups/pre-v0.1.14-20260822153000.sql.tag
```

| Archivo | Contenido |
| --- | --- |
| `.sql` | Dump de la BD **justo antes** de desplegar `v0.1.14` (estado compatible con la versión anterior) |
| `.tag` | Tag de la API que estaba corriendo cuando se tomó ese backup (ej. `v0.1.12`) |

El dump se genera con `pg_dump --clean --if-exists`, así que el SQL limpia y recrea objetos; no hace falta un `DROP DATABASE` manual.

## Flujo completo (ejemplo)

Estado actual:

- API → `v0.1.14`
- BD → esquema/datos de `v0.1.14`
- Bug descubierto **después** de un deploy exitoso

### 1. Detener la API

Evita escrituras mientras restauras:

```bash
cd /opt/molaryx
docker compose stop api
```

### 2. Restaurar el backup

```bash
POSTGRES_USER=$(grep '^POSTGRES_USER=' .env | cut -d '=' -f2-)
POSTGRES_DB=$(grep '^POSTGRES_DB=' .env | cut -d '=' -f2-)

docker compose exec -T postgres \
  psql \
  -U "$POSTGRES_USER" \
  -d "$POSTGRES_DB" \
  -v ON_ERROR_STOP=1 \
  < /opt/molaryx/backups/pre-v0.1.14-20260822153000.sql
```

Sustituye la ruta del `.sql` por el backup que corresponda al deploy que quieres revertir.

### 3. Volver a la versión de API del `.tag`

```bash
cat /opt/molaryx/backups/pre-v0.1.14-20260822153000.sql.tag
# ejemplo: v0.1.12

PREVIOUS_TAG=$(cat /opt/molaryx/backups/pre-v0.1.14-20260822153000.sql.tag)

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

BACKUP=/opt/molaryx/backups/pre-v0.1.14-20260822153000.sql

docker compose stop api

POSTGRES_USER=$(grep '^POSTGRES_USER=' .env | cut -d '=' -f2-)
POSTGRES_DB=$(grep '^POSTGRES_DB=' .env | cut -d '=' -f2-)

docker compose exec -T postgres \
  psql \
  -U "$POSTGRES_USER" \
  -d "$POSTGRES_DB" \
  -v ON_ERROR_STOP=1 \
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
| Estado de PostgreSQL | Restaurar el `.sql` correspondiente con `psql` |
| Qué API va con ese dump | Leer el `.tag` junto al backup |

**No** ejecutes el tag anterior esperando que eso restaure la BD. Son dos operaciones distintas: código vía deploy/tag, datos vía backup SQL.

## Nota

Esto asume que vuelves a la pareja **API del `.tag` + BD del `.sql`**. Tras restaurar el dump, no vuelvas a levantar la versión nueva sin migrar de nuevo: esquema y código deben quedar alineados.

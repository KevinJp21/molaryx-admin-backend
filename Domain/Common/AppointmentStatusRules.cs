using Domain.Enums;

namespace Domain.Common
{
    public static class AppointmentStatusRules
    {
        private static readonly HashSet<AppointmentStatusEnum> BlockingStatuses =
        [
            AppointmentStatusEnum.PENDING,
            AppointmentStatusEnum.CONFIRMED,
            AppointmentStatusEnum.IN_PROGRESS
        ];

        private static readonly Dictionary<AppointmentStatusEnum, HashSet<AppointmentStatusEnum>> AllowedTransitions =
            new()
            {
                [AppointmentStatusEnum.PENDING] =
                [
                    AppointmentStatusEnum.CONFIRMED,
                    AppointmentStatusEnum.CANCELLED
                ],
                [AppointmentStatusEnum.CONFIRMED] =
                [
                    AppointmentStatusEnum.COMPLETED,
                    AppointmentStatusEnum.CANCELLED,
                    AppointmentStatusEnum.NO_SHOW
                ],
                [AppointmentStatusEnum.IN_PROGRESS] =
                [
                    AppointmentStatusEnum.COMPLETED,
                    AppointmentStatusEnum.CANCELLED,
                    AppointmentStatusEnum.NO_SHOW
                ],
                [AppointmentStatusEnum.COMPLETED] = [],
                [AppointmentStatusEnum.CANCELLED] = [],
                [AppointmentStatusEnum.NO_SHOW] = []
            };

        private static readonly Dictionary<AppointmentStatusEnum, string> DisplayNames = new()
        {
            [AppointmentStatusEnum.PENDING] = "Pendiente",
            [AppointmentStatusEnum.CONFIRMED] = "Confirmado",
            [AppointmentStatusEnum.IN_PROGRESS] = "En progreso",
            [AppointmentStatusEnum.COMPLETED] = "Completado",
            [AppointmentStatusEnum.CANCELLED] = "Cancelado",
            [AppointmentStatusEnum.NO_SHOW] = "No se presentó"
        };

        public static bool IsBlocking(short idAppointmentStatus)
            => IsBlocking((AppointmentStatusEnum)idAppointmentStatus);

        public static bool IsBlocking(AppointmentStatusEnum status)
            => BlockingStatuses.Contains(status);

        public static string GetDisplayName(AppointmentStatusEnum status)
            => DisplayNames.TryGetValue(status, out var name) ? name : status.ToString();

        public static bool IsFinal(short idAppointmentStatus)
            => IsFinal((AppointmentStatusEnum)idAppointmentStatus);

        public static bool IsFinal(AppointmentStatusEnum status)
            => status is AppointmentStatusEnum.COMPLETED
                or AppointmentStatusEnum.CANCELLED
                or AppointmentStatusEnum.NO_SHOW;

        public static void EnsureCanEdit(short idAppointmentStatus)
            => EnsureCanEdit((AppointmentStatusEnum)idAppointmentStatus);

        public static void EnsureCanEdit(AppointmentStatusEnum status)
        {
            if (IsFinal(status))
            {
                throw new InvalidOperationException(
                    $"No se puede editar una cita en estado {GetDisplayName(status)}.");
            }
        }

        public static void EnsureCanTransition(short fromStatus, short toStatus)
            => EnsureCanTransition(
                (AppointmentStatusEnum)fromStatus,
                (AppointmentStatusEnum)toStatus);

        public static void EnsureCanTransition(
            AppointmentStatusEnum from,
            AppointmentStatusEnum to)
        {
            if (from == to)
            {
                return;
            }

            if (!Enum.IsDefined(from) || !Enum.IsDefined(to))
            {
                throw new InvalidOperationException("El estado de la cita no es válido.");
            }

            if (!AllowedTransitions.TryGetValue(from, out var allowed) || !allowed.Contains(to))
            {
                throw new InvalidOperationException(
                    $"No se puede cambiar la cita de {GetDisplayName(from)} a {GetDisplayName(to)}.");
            }
        }
    }
}

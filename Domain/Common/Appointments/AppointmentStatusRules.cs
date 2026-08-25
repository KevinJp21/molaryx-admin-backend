using Domain.Enums;

namespace Domain.Common.Appointments
{
    public static class AppointmentStatusRules
    {
        private static readonly HashSet<AppointmentStatusEnum> BlockingStatuses =
        [
            AppointmentStatusEnum.PENDING,
            AppointmentStatusEnum.CONFIRMED,
            AppointmentStatusEnum.IN_PROGRESS
        ];

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

        public static bool CanReceivePayment(short idAppointmentStatus)
            => CanReceivePayment((AppointmentStatusEnum)idAppointmentStatus);

        public static bool CanReceivePayment(AppointmentStatusEnum status)
            => status is not (AppointmentStatusEnum.CANCELLED or AppointmentStatusEnum.NO_SHOW);

        public static void EnsureCanReceivePayment(short idAppointmentStatus)
            => EnsureCanReceivePayment((AppointmentStatusEnum)idAppointmentStatus);

        public static void EnsureCanReceivePayment(AppointmentStatusEnum status)
        {
            if (!Enum.IsDefined(status))
            {
                throw new InvalidOperationException("El estado de la cita no es válido.");
            }

            if (!CanReceivePayment(status))
            {
                throw new InvalidOperationException(
                    $"No se puede registrar un pago en una cita en estado {GetDisplayName(status)}.");
            }
        }
    }
}

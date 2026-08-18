using Domain.Enums;

namespace Domain.Common
{
    public static class TreatmentStatusRules
    {
        private static readonly HashSet<TreatmentStatusEnum> BlockingStatuses =
        [
            TreatmentStatusEnum.ACTIVE,
            TreatmentStatusEnum.PAUSED,
            TreatmentStatusEnum.COMPLETED,
            TreatmentStatusEnum.CANCELLED
        ];

        private static readonly Dictionary<TreatmentStatusEnum, HashSet<TreatmentStatusEnum>> AllowedTransitions =
            new()
            {
                [TreatmentStatusEnum.ACTIVE] =
                [
                    TreatmentStatusEnum.PAUSED,
                    TreatmentStatusEnum.COMPLETED,
                    TreatmentStatusEnum.CANCELLED
                ],
                [TreatmentStatusEnum.PAUSED] =
                [
                    TreatmentStatusEnum.ACTIVE,
                    TreatmentStatusEnum.CANCELLED
                ],
                [TreatmentStatusEnum.COMPLETED] = [],
                [TreatmentStatusEnum.CANCELLED] = [],
            };

        private static readonly Dictionary<TreatmentStatusEnum, string> DisplayNames = new()
        {
            [TreatmentStatusEnum.ACTIVE] = "Activo",
            [TreatmentStatusEnum.PAUSED] = "Pausado",
            [TreatmentStatusEnum.COMPLETED] = "Completado",
            [TreatmentStatusEnum.CANCELLED] = "Cancelado"
        };

        public static bool IsBlocking(short idTreatmentStatus)
            => IsBlocking((TreatmentStatusEnum)idTreatmentStatus);

        public static bool IsBlocking(TreatmentStatusEnum status)
            => BlockingStatuses.Contains(status);

        public static string GetDisplayName(TreatmentStatusEnum status)
            => DisplayNames.TryGetValue(status, out var name) ? name : status.ToString();

        public static bool IsFinal(short idTreatmentStatus)
            => IsFinal((TreatmentStatusEnum)idTreatmentStatus);

        public static bool IsFinal(TreatmentStatusEnum status)
            => status is TreatmentStatusEnum.COMPLETED
                or TreatmentStatusEnum.CANCELLED;

        public static void EnsureCanEdit(short idTreatmentStatus)
            => EnsureCanEdit((TreatmentStatusEnum)idTreatmentStatus);

        public static void EnsureCanEdit(TreatmentStatusEnum status)
        {
            if (IsFinal(status))
            {
                throw new InvalidOperationException(
                    $"No se puede editar un tratamiento en estado {GetDisplayName(status)}.");
            }
        }

        public static bool CanReceivePayment(short idTreatmentStatus)
            => CanReceivePayment((TreatmentStatusEnum)idTreatmentStatus);

        public static bool CanReceivePayment(TreatmentStatusEnum status)
            => status is not TreatmentStatusEnum.CANCELLED;

        public static void EnsureCanReceivePayment(short idTreatmentStatus)
            => EnsureCanReceivePayment((TreatmentStatusEnum)idTreatmentStatus);

        public static void EnsureCanReceivePayment(TreatmentStatusEnum status)
        {
            if (!Enum.IsDefined(status))
            {
                throw new InvalidOperationException("El estado del tratamiento no es válido.");
            }

            if (!CanReceivePayment(status))
            {
                throw new InvalidOperationException(
                    $"No se puede registrar un pago en un tratamiento en estado {GetDisplayName(status)}.");
            }
        }

        public static bool CanLinkAppointment(short idTreatmentStatus)
            => CanLinkAppointment((TreatmentStatusEnum)idTreatmentStatus);

        public static bool CanLinkAppointment(TreatmentStatusEnum status)
            => status is TreatmentStatusEnum.ACTIVE;

        public static void EnsureCanLinkAppointment(short idTreatmentStatus)
            => EnsureCanLinkAppointment((TreatmentStatusEnum)idTreatmentStatus);

        public static void EnsureCanLinkAppointment(TreatmentStatusEnum status)
        {
            if (!Enum.IsDefined(status))
            {
                throw new InvalidOperationException("El estado del tratamiento no es válido.");
            }

            if (!CanLinkAppointment(status))
            {
                throw new InvalidOperationException(
                    $"No se puede asociar una cita a un tratamiento en estado {GetDisplayName(status)}.");
            }
        }

        public static void EnsureCanTransition(short fromStatus, short toStatus)
            => EnsureCanTransition(
                (TreatmentStatusEnum)fromStatus,
                (TreatmentStatusEnum)toStatus);

        public static void EnsureCanTransition(
            TreatmentStatusEnum from,
            TreatmentStatusEnum to)
        {
            if (from == to)
            {
                return;
            }

            if (!Enum.IsDefined(from) || !Enum.IsDefined(to))
            {
                throw new InvalidOperationException("El estado del tratamiento no es válido.");
            }

            if (!AllowedTransitions.TryGetValue(from, out var allowed) || !allowed.Contains(to))
            {
                throw new InvalidOperationException(
                    $"No se puede cambiar el tratamiento de {GetDisplayName(from)} a {GetDisplayName(to)}.");
            }
        }
    }
}

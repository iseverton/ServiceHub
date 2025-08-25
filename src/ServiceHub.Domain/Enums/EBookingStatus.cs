using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceHub.Domain.Enums;

public enum EBookingStatus
{
    Pending = 0,     // Cliente pediu, prestador ainda não confirmou
    Confirmed = 1,   // Prestador aceitou
    InProgress = 2,  // Serviço está sendo realizado
    Finished = 3,   // Serviço finalizado
    Cancelled = 4    // Cancelado antes da execução
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.DAL.Models
{
    public enum Status
    {
        Geen,
        Uitgevoerd,
        In_Behandeling,
        Afgekeurd_Te_Weinig_Saldo,
        Afgekeurd_Anders,
        Periodiek
    }
}

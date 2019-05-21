using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringClient.Repository
{
    public interface IRepositoryBase<M>
    {
        string ConnectionString { get;}
        /// <summary>
        /// Zählt in der Datenbank die Anzahl Model-Objekte vom Typ M, die der
        /// Where-Bedingung entsprechen
        /// </summary>
        /// <param name="whereCondition">WhereBedingung als string
        /// z.B. "NetPrice > @netPrice and Active = @active and
        /// Description like @desc</param>
        /// <param name="parameterValues">Parameter-Werte für die Wherebedingung
        /// bspw: {{"netPrice", 10.5}, {"active", true}, {"desc", "Wolle%"}}</param>
        /// <returns></returns>
        long Count(string whereCondition, Dictionary<string, object> parameterValues);
        /// <summary>
        /// Zählt alle Model-Objekte vom Typ M
        /// </summary>
        /// <returns></returns>
        long Count();
        /// <summary>
        /// Gibt den Tabellennamen zurück, auf die sich das Repository bezieht
        /// </summary>
        string TableName { get; }
    }
}

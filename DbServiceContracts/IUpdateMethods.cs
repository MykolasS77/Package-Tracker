using ModelsLibrary.DTOs;
using ModelsLibrary.Models;

namespace DbServiceContracts
{
    public interface IUpdateMethods
    {
        /// <summary>
        /// Updates current package status by adding a new status to history table.
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public StatusHistory AddTimestamp(StatusHistoryRequest newItem);
    }
}

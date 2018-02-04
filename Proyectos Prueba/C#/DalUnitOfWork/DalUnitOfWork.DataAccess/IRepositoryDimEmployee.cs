namespace DalUnitOfWork.DataAccess
{
    using Models.Model;

    using System.Collections.Generic;

    public interface IRepositoryDimEmployee : IRepository<DimEmployee>
    {
        IEnumerable<DimEmployee> GetTopVacationsHours(int top);

        IEnumerable<DimEmployee> GetTopSickHours();
    }
}

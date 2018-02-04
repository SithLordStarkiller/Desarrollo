namespace DalUnitOfWork.DataAccess
{
    using Models.Model;
    
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class RepositoryDimEmployee : Repository<DimEmployee>, IRepositoryDimEmployee
    {
        public AdventureworksEntities EmpContext
        {
            get { return Context as AdventureworksEntities; }
        }

        public RepositoryDimEmployee(AdventureworksEntities context) : base(context)
        {
        }

        public IEnumerable<DimEmployee> GetTopVacationsHours(int top)
        {
            return EmpContext.DimEmployee.ToList().OrderBy(c => c.SickLeaveHours).Take(top);
        }

        public IEnumerable<DimEmployee> GetTopSickHours()
        {
            return EmpContext.DimEmployee.ToList().OrderBy(c => c.VacationHours);
        }
    }
}

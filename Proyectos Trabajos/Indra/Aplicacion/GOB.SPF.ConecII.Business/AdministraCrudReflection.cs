using GOB.SPF.ConecII.AccessData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Business
{
    public class AdministraCrudReflection<TEntity, TViewModel> : AdministraReflection<TEntity>
        where TEntity : class
        where TViewModel : class
    {


        public override void Inicializa(Repositorio<TEntity> repositorio)
        {
            base.Inicializa(repositorio);
        }

    }
}

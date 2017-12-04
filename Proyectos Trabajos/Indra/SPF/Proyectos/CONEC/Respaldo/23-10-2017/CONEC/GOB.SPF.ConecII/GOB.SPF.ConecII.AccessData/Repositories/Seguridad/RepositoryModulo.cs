using GOB.SPF.ConecII.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.AccessData;
using GOB.SPF.ConecII.AccessData.Schemas;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Interfaces.Genericos;

namespace GOB.SPF.ConecII.AccessData.Repositories.Modulos
{
    public class RepositoryModulo : IRepository<IModulo>
    {
        public RepositoryModulo(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private IUnitOfWork _unitOfWork;
        public int Actualizar(IModulo entity)
        {
            throw new NotImplementedException();
        }

        public int CambiarEstatus(IModulo entity)
        {
            throw new NotImplementedException();
        }

        public int Insertar(IModulo entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IModulo> Obtener(IPaging paging)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IModulo> ObtenerPorCriterio(IPaging paging, IModulo entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IModulo> ObtenerTodos(int? moduloPadre = null)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                var modulos = new List<Entities.Modulos.Modulo>();
                cmd.CommandText = Seguridad.ModulosRolesObtener;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter { ParameterName = "@IdPadre", Value = moduloPadre ?? (object)DBNull.Value});

                using (var reader = cmd.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        var id = reader.GetValueFromDbObject<int>("IdModulo");
                        var modulo = new Entities.Modulos.Modulo
                        {
                            Id = id,
                            SubModulos = ObtenerTodos(id),
                            Descripcion = reader.GetValueFromDbObject<string>("Descripcion"),
                            Accion = reader.GetValueFromDbObject<string>("Accion"),
                            Controlador = reader.GetValueFromDbObject<string>("Controlador"),
                            Activo = reader.GetValueFromDbObject<bool>("Activo"),
                            FechaFinal = reader.GetValueFromDbObject<DateTime?>("FechaFinal"),
                            FechaInicial = reader.GetValueFromDbObject<DateTime>("FechaInicial"),
                            IdPadre = reader.GetValueFromDbObject<int?>("IdPadre"),
                            Nombre = reader.GetValueFromDbObject<string>("Nombre")
                        };

                        modulos.Add(modulo);
                    }
                }

                return modulos;
            }

        }
    

        public IModulo ObtenerPorId(long Identificador)
        {
            throw new NotImplementedException();
        }
    }
}

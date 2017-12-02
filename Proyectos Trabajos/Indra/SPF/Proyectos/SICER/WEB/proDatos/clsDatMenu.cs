using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using proEntidad;

namespace proDatos
{
    public class clsDatMenu
    {
        public static List<TreeNode> MenuContextual(string tipoOperacion, byte idRol)
        {
            sicerEntities context_Entiti;
            List<TreeNode> treeNodeList = new List<TreeNode>();

            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity();

            using (context_Entiti = new sicerEntities(strConnection_Entiti))
            {
                #region Persiste BD
                var lt = (from a in context_Entiti.spuConsultarMenuContextual(tipoOperacion, idRol)
                          select new clsEntMenuCatalogo
                          {
                              idMenu = a.idMenu,
                              menDescripcion = a.menDescripcion,
                              activo = Convert.ToBoolean(a.menActivo),
                              menPadre = a.menContenedor,
                              tipo = Convert.ToBoolean(a.menLeaf),
                              menIcon = a.menIcon,
                              menIconContenedor = a.menIconContenedor,
                              pmCaptura = Convert.ToBoolean(a.pmCaptura),
                              pmModifica = Convert.ToBoolean(a.pmModifica)


                          }).ToList();
                #endregion

               // int count = 0;
                foreach (var item in lt)
                {
                    #region Padres sin hijos
                    //if (item.tipo == true)
                    //{
                        treeNodeList.Add(new TreeNode
                        {
                            pmCaptura = item.pmCaptura,
                            pmModifica = item.pmModifica,
                            idMenu = item.idMenu,
                            text = item.menDescripcion,
                            leaf = true,
                            iconCls = item.menIcon
                        });
                    //count = count + 1;
                    //}
                    #endregion
                    /*
                    #region Padres con Hijos
                    if ((from a in treeNodeList where a.text == item.menPadre select a).Count() == 0)
                    {
                        if (item.menPadre != "-")
                        {
                            treeNodeList.Add(new TreeNode
                            {
                                idMenu = item.idMenu,
                                pmCaptura = item.pmCaptura,
                                pmModifica = item.pmModifica,
                                text = item.menPadre,
                                iconCls = item.menIconContenedor,
                                expanded = true
                            });

                            var children = (from a in lt where a.menPadre == item.menPadre select a);

                            foreach (var childenAdd in children)
                            {
                                treeNodeList[count].children.Add(new TreeNode
                                {
                                    pmCaptura = childenAdd.pmCaptura,
                                    pmModifica = childenAdd.pmModifica,
                                    idMenu = childenAdd.idMenu,
                                    text = childenAdd.menDescripcion,
                                    leaf = true,
                                    iconCls = childenAdd.menIcon
                                });
                            }
                            count = count + 1;
                        }

                    }
                    #endregion
                    */
                }
            };

            return treeNodeList;
        }


        public static List<TreeNode> menuCertificaciones()
        {
            sicerEntities context_Entiti;
            List<TreeNode> treeNodeList = new List<TreeNode>();

            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity();

            using (context_Entiti = new sicerEntities(strConnection_Entiti))
            {
                #region Persiste BD
      
                var lista = context_Entiti.spuConsultarCertificaciones().ToList();

                #endregion

                foreach (var cert in lista)
                {
                    #region Padres sin hijos
                        treeNodeList.Add(new TreeNode
                        {
                            idMenu = cert.idCertificacion,
                            text = cert.certificacion,
                            leaf = true
                        });
                    #endregion
        
                }
            };

            return treeNodeList;
        }

        public static List<TreeNode> rolPermiso(byte idRol)
        {
            sicerEntities context_Entiti;
            List<TreeNode> treeNodeList = new List<TreeNode>();

            string strConnection_Entiti = clsDatConexion.getConnectionString_Entity();

            using (context_Entiti = new sicerEntities(strConnection_Entiti))
            {
                #region Persiste BD

                var list = context_Entiti.spuPermisoRol(idRol).ToList();

                #endregion

                foreach (var rol in list)
                {
                    #region Padres sin hijos
                    treeNodeList.Add(new TreeNode
                    {
                        pmCaptura = Convert.ToBoolean(rol.permiso),
                        text = rol.menDescripcion,
                        leaf = true
                    });
                    #endregion
                }
            };
            return treeNodeList;
        }

    }
}

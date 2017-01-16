using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PubliPayments.App_Code;
using PubliPayments.Entidades;
using PubliPayments.Negocios;
using PubliPayments.Utiles;

namespace PubliPayments.Controllers
{
    public class GestionMovilController : Controller
    {
        filtroDashBoard _tipoDashBoard = filtroDashBoard.Sin_Asignacion;
        private string _delegacionUsuario = "";

        public void Inicializar()
        {
            _delegacionUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol)) == 5
                ? new EntRankingIndicadores().ObtenerDelegacionUsuario(
                    SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario))[0]
                : "";

            switch (Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol)))
            {
                case 0: _tipoDashBoard = filtroDashBoard.Administrador; break;
                case 1: _tipoDashBoard = filtroDashBoard.Administrador; break;
                case 2: _tipoDashBoard = filtroDashBoard.Despacho; break;
                case 3: _tipoDashBoard = filtroDashBoard.Supervisor; break;
                case 4: _tipoDashBoard = filtroDashBoard.Gestor; break;
                case 5: _tipoDashBoard = filtroDashBoard.Delegacion; break;
                case 6: _tipoDashBoard = filtroDashBoard.Direccion; break;
            }
            ViewData["tpFrm"] = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            ViewData["tipoFormulario"] = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Descripcion;
        }

        public ActionResult Index()
        {
            Inicializar();
            ViewBag.tipoDashBoard = _tipoDashBoard.ToString();
            ViewData["tipoDashBoard"] = _tipoDashBoard.ToString();
            ViewBag.delegacion = _delegacionUsuario;
            ViewData["delegacion"] = _delegacionUsuario;
            ViewBag.despacho = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio);
            ViewData["despacho"] = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio);
            ViewBag.usuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            ViewData["usuario"] = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));

            ViewBag.IdRol = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));

            return View("Index");
        }

        public ActionResult TabPartial(string tipoDashboard)
        {            
            return PartialView("tabPartial");
        }

        public String ObtenerFechaActualizacion()
        {
            return new EntGestionMovil().ObtenerFechaActualizacion();
        }

        public ActionResult CreaComboFiltro(string idCombo)
        {

            DataSet lista1 = null;
            switch (idCombo)
            {
                case "horaGestionCombo":
                    lista1 = new EntGestionMovil().ObtenerHoraGestionFiltro();
                    break;
                case "diaGestionCombo":
                    lista1 = new EntGestionMovil().ObtenerDiaGestionFiltro();
                    break;
                case "estFinalCombo":
                    lista1 = new EntGestionMovil().ObtenerEstFinalFiltro();
                    break;
                case "delegacionCombo":
                    lista1 = new EntGestionMovil().ObtenerDelegacionesFiltro();
                    break;
                case "fechaCargaCombo":
                    lista1 = new EntGestionMovil().ObtenerFechaCargaFiltro();
                    break;
            }

            var lista = new List<OpcionesFiltroDashboard>();

            if (lista1 == null) return PartialView(lista);
            lista.AddRange(from DataRow row in lista1.Tables[0].Rows
                           select new OpcionesFiltroDashboard
                           {
                               Value = row["Value"].ToString(),
                               Description = row["Description"].ToString()
                           });

            return PartialView(lista);
        }

        #region GestionXHora
        public ActionResult GestionXHora(string delegacion, string fechaCarga, string resFinal, string diaGestion)
        {
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntGestionMovil().GestionXHoraMaster(delegacion ?? "", fechaCarga ?? "", resFinal ?? "",
                diaGestion ?? "", tipoFormulario??"");
            var columnasCancelar = "";
            var tabla = new DataTable();
            tabla.Columns.Add(new DataColumn { ColumnName = "idDespacho" });
            tabla.Columns.Add(new DataColumn {ColumnName = "Despacho"});
            tabla.Columns.Add(new DataColumn { ColumnName = "Sin fecha de gestion" });

            if (lista.Tables[1].Rows.Count > 0)
            {
                for (var i = 2; i < 34; i++)
                {
                    if (lista.Tables[1].Rows[0][i].ToString() != "0")
                    {
                        var columna = new DataColumn
                        {
                            ColumnName = ("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture))
                        };
                        tabla.Columns.Add(columna);
                        columnasCancelar += ((i - 2) + "|");
                    }
                }
            }
            var columna1 = new DataColumn { ColumnName = "Total" };
            tabla.Columns.Add(columna1);

            var listaColumnas = columnasCancelar.Split('|');
            foreach (DataRow row in lista.Tables[0].Rows)
            {
                var totalFila = 0;
                var fila = tabla.NewRow();
                fila["idDespacho"]=row["idDominio"];
                fila["Despacho"] = row["NombreDominio"];
                fila["Sin fecha de gestion"] = row["valor32"];
                for (var d=0;d<(listaColumnas.Length-1);d++)
                {
                    fila["Valor"+listaColumnas[d].ToString(CultureInfo.InvariantCulture)] = row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)];
                    totalFila += Convert.ToInt32(row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)]);
                }
                totalFila += Convert.ToInt32(row["valor32"]);
                fila["Total"] = totalFila.ToString(CultureInfo.InvariantCulture);
                tabla.Rows.Add(fila);

            }

            var totalFila2 = 0;
            var fila2 = tabla.NewRow();
            fila2["idDespacho"] = 0;
            fila2["Despacho"] = "Total";
            if (lista.Tables[1].Rows.Count > 0)
            {
                fila2["Sin fecha de gestion"] = lista.Tables[1].Rows[0][1];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila2["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] =
                        lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value
                            ? "0"
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)];
                    totalFila2 +=
                        Convert.ToInt32(lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] ==
                                        DBNull.Value
                            ? 0
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)]);
                }
                totalFila2 +=
                    Convert.ToInt32(lista.Tables[1].Rows[0][1] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][1]);
            }
            fila2["Total"] = totalFila2.ToString(CultureInfo.InvariantCulture);
            tabla.Rows.Add(fila2);

            if (totalFila2 == 0)
            {
                tabla.Clear();
            }

            ViewData["listaColumnas"] = listaColumnas;
            ViewData["delegacion"]= delegacion;
            ViewData["fechaCarga"]=fechaCarga;
            ViewData["resFinal"]=resFinal;
            ViewData["diaGestion"] = diaGestion;
            return PartialView("GestionXHora",tabla);
        }

        public ActionResult GestionXHoraDespacho(string delegacion, string fechaCarga, string resFinal, string diaGestion, string despacho)
        {
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntGestionMovil().GestionXHoraDespacho(delegacion ?? "", fechaCarga ?? "", resFinal ?? "", diaGestion ?? "", despacho, tipoFormulario??"");
            var columnasCancelar = "";
            var tabla = new DataTable();
            tabla.Columns.Add(new DataColumn { ColumnName = "idUsuarioPadre" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Usuario" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Supervisor" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Sin fecha de gestion" });

            if (lista.Tables[1].Rows.Count > 0)
            {
                for (var i = 2; i < 34; i++)
                {

                    if (lista.Tables[1].Rows[0][i].ToString() != "0")
                    {
                        var columna = new DataColumn
                        {
                            ColumnName = ("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture))
                        };
                        tabla.Columns.Add(columna);
                        columnasCancelar += ((i - 2) + "|");
                    }
                }
            }
            var columna1 = new DataColumn { ColumnName = "Total" };
            tabla.Columns.Add(columna1);

            var listaColumnas = columnasCancelar.Split('|');
            foreach (DataRow row in lista.Tables[0].Rows)
            {
                var totalFila = 0;
                var fila = tabla.NewRow();
                fila["idUsuarioPadre"] = row["idUsuarioPadre"];
                fila["Supervisor"] = row["Nombre"];
                fila["Usuario"] = row["Usuario"];
                fila["Sin fecha de gestion"] = row["valor32"];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] = row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)];
                    totalFila += Convert.ToInt32(row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)]);
                }
                totalFila += Convert.ToInt32(row["valor32"]);
                fila["Total"] = totalFila.ToString(CultureInfo.InvariantCulture);
                tabla.Rows.Add(fila);

            }

            var totalFila2 = 0;
            var fila2 = tabla.NewRow();
            fila2["idUsuarioPadre"] = 0;
            fila2["Usuario"] = "";
            fila2["Supervisor"] = "Total";
            if (lista.Tables[1].Rows.Count > 0)
            {
                if (lista.Tables[1].Rows.Count > 0)
                {
                    fila2["Sin fecha de gestion"] = lista.Tables[1].Rows[0][1];
                }
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila2["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] =
                        lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value
                            ? "0"
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)];
                    totalFila2 +=
                        Convert.ToInt32(lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] ==
                                        DBNull.Value
                            ? 0
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)]);
                }
                totalFila2 +=
                    Convert.ToInt32(lista.Tables[1].Rows[0][1] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][1]);
            }
            fila2["Total"] = totalFila2.ToString(CultureInfo.InvariantCulture);
            tabla.Rows.Add(fila2);

            if (totalFila2 == 0)
            {
                tabla.Clear();
            }

            ViewData["listaColumnas"] = listaColumnas;
            ViewData["delegacion"] = delegacion;
            ViewData["fechaCarga"] = fechaCarga;
            ViewData["resFinal"] = resFinal;
            ViewData["diaGestion"] = diaGestion;
            ViewData["despacho"] = despacho;
            return PartialView("GestionXHoraDespacho", tabla);
            
        }

        public ActionResult GestionXHoraSupervisor(string delegacion, string fechaCarga, string resFinal, string diaGestion, string despacho, string supervisor)
        {
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntGestionMovil().GestionXHoraSupervisor(delegacion ?? "", fechaCarga ?? "", resFinal ?? "", diaGestion ?? "", despacho, supervisor, tipoFormulario??"");
            var columnasCancelar = "";
            var tabla = new DataTable();
            tabla.Columns.Add(new DataColumn { ColumnName = "idUsuario" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Usuario" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Gestor" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Sin fecha de gestion" });

            if (lista.Tables[1].Rows.Count > 0)
            {
                for (var i = 2; i < 34; i++)
                {
                    if (lista.Tables[1].Rows[0][i].ToString() != "0")
                    {
                        var columna = new DataColumn
                        {
                            ColumnName = ("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture))
                        };
                        tabla.Columns.Add(columna);
                        columnasCancelar += ((i - 2) + "|");
                    }
                }
            }
            var columna1 = new DataColumn { ColumnName = "Total" };
            tabla.Columns.Add(columna1);

            var listaColumnas = columnasCancelar.Split('|');
            foreach (DataRow row in lista.Tables[0].Rows)
            {
                var totalFila = 0;
                var fila = tabla.NewRow();
                fila["idUsuario"] = row["idUsuario"];
                fila["Usuario"] = row["Usuario"];
                fila["Gestor"] = row["Nombre"];
                fila["Sin fecha de gestion"] = row["valor32"];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] = row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)];
                    totalFila += Convert.ToInt32(row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)]);
                }
                totalFila += Convert.ToInt32(row["valor32"]);
                fila["Total"] = totalFila.ToString(CultureInfo.InvariantCulture);
                tabla.Rows.Add(fila);

            }

            var totalFila2 = 0;
            var fila2 = tabla.NewRow();
            fila2["idUsuario"] = 0;
            fila2["Usuario"] = "";
            fila2["Gestor"] = "Total";
            if (lista.Tables[1].Rows.Count > 0)
            {
                fila2["Sin fecha de gestion"] = lista.Tables[1].Rows[0][1];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila2["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] =
                        lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value
                            ? "0"
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)];
                    totalFila2 +=
                        Convert.ToInt32(lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] ==
                                        DBNull.Value
                            ? 0
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)]);
                }
                totalFila2 +=
                    Convert.ToInt32(lista.Tables[1].Rows[0][1] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][1]);
            }
            fila2["Total"] = totalFila2.ToString(CultureInfo.InvariantCulture);
            tabla.Rows.Add(fila2);

            if (totalFila2 == 0)
            {
                tabla.Clear();
            }

            ViewData["listaColumnas"] = listaColumnas;
            ViewData["delegacion"] = delegacion;
            ViewData["fechaCarga"] = fechaCarga;
            ViewData["resFinal"] = resFinal;
            ViewData["diaGestion"] = diaGestion;
            ViewData["despacho"] = despacho;
            ViewData["supervisor"] = supervisor;
            return PartialView("GestionXHoraSupervisor", tabla);
            
        }
        #endregion

        #region GestionXDia
        public ActionResult GestionXDia(string delegacion, string fechaCarga, string resFinal, string horaGestion)
        {
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntGestionMovil().GestionXDiaMaster(delegacion ?? "", fechaCarga ?? "", resFinal ?? "", horaGestion ?? "", tipoFormulario??"");
            var columnasCancelar = "";
            var tabla = new DataTable();
            tabla.Columns.Add(new DataColumn { ColumnName = "idDespacho" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Despacho" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Sin fecha de gestion" });

            if (lista.Tables[1].Rows.Count > 0)
            {
                for (var i = 2; i < 34; i++)
                {
                    if (lista.Tables[1].Rows[0][i].ToString() != "0")
                    {
                        var columna = new DataColumn
                        {
                            ColumnName = ("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture))
                        };
                        tabla.Columns.Add(columna);
                        columnasCancelar += ((i - 2) + "|");
                    }
                }
            }
            var columna1 = new DataColumn { ColumnName = "Total" };
            tabla.Columns.Add(columna1);

            var listaColumnas = columnasCancelar.Split('|');
            foreach (DataRow row in lista.Tables[0].Rows)
            {
                var totalFila = 0;
                var fila = tabla.NewRow();
                fila["idDespacho"] = row["idDominio"];
                fila["Despacho"] = row["NombreDominio"];
                fila["Sin fecha de gestion"] = row["valor32"];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] = row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)];
                    totalFila += Convert.ToInt32(row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)]);
                }
                totalFila += Convert.ToInt32(row["valor32"]);
                fila["Total"] = totalFila.ToString(CultureInfo.InvariantCulture);
                tabla.Rows.Add(fila);

            }

            var totalFila2 = 0;
            var fila2 = tabla.NewRow();
            fila2["idDespacho"] = 0;
            fila2["Despacho"] = "Total";
            if (lista.Tables[1].Rows.Count > 0)
            {
                fila2["Sin fecha de gestion"] = lista.Tables[1].Rows[0][1];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila2["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] =
                        lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value
                            ? "0"
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)];
                    totalFila2 +=
                        Convert.ToInt32(lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] ==
                                        DBNull.Value
                            ? 0
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)]);
                }
                totalFila2 +=
                    Convert.ToInt32(lista.Tables[1].Rows[0][1] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][1]);
            }
            fila2["Total"] = totalFila2.ToString(CultureInfo.InvariantCulture);
            tabla.Rows.Add(fila2);

            if (totalFila2 == 0)
            {
                tabla.Clear();
            }

            ViewData["listaColumnas"] = listaColumnas;
            ViewData["delegacion"] = delegacion;
            ViewData["fechaCarga"] = fechaCarga;
            ViewData["resFinal"] = resFinal;
            ViewData["horaGestion"] = horaGestion;
            return PartialView("GestionXDia",tabla);
        }

        public ActionResult GestionXDiaDespacho(string delegacion, string fechaCarga, string resFinal, string horaGestion, string despacho)
        {
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntGestionMovil().GestionXDiaDespacho(delegacion ?? "", fechaCarga ?? "", resFinal ?? "", horaGestion ?? "", despacho, tipoFormulario??"");
            var columnasCancelar = "";
            var tabla = new DataTable();
            tabla.Columns.Add(new DataColumn { ColumnName = "idUsuarioPadre" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Usuario" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Supervisor" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Sin fecha de gestion" });

            if (lista.Tables[1].Rows.Count > 0)
            {
                for (var i = 2; i < 34; i++)
                {
                    if (lista.Tables[1].Rows[0][i].ToString() != "0")
                    {
                        var columna = new DataColumn
                        {
                            ColumnName = ("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture))
                        };
                        tabla.Columns.Add(columna);
                        columnasCancelar += ((i - 2) + "|");
                    }
                }
            }
            var columna1 = new DataColumn { ColumnName = "Total" };
            tabla.Columns.Add(columna1);

            var listaColumnas = columnasCancelar.Split('|');
            foreach (DataRow row in lista.Tables[0].Rows)
            {
                var totalFila = 0;
                var fila = tabla.NewRow();
                fila["idUsuarioPadre"] = row["idUsuarioPadre"];
                fila["Usuario"] = row["Usuario"];
                fila["Supervisor"] = row["Nombre"];
                fila["Sin fecha de gestion"] = row["valor32"];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] = row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)];
                    totalFila += Convert.ToInt32(row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)]);
                }
                totalFila += Convert.ToInt32(row["valor32"]);
                fila["Total"] = totalFila.ToString(CultureInfo.InvariantCulture);
                tabla.Rows.Add(fila);

            }

            var totalFila2 = 0;
            var fila2 = tabla.NewRow();
            fila2["idUsuarioPadre"] = 0;
            fila2["Usuario"] = "";
            fila2["Supervisor"] = "Total";
            if (lista.Tables[1].Rows.Count > 0)
            {
                fila2["Sin fecha de gestion"] = lista.Tables[1].Rows[0][1];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila2["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] =
                        lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value
                            ? "0"
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)];
                    totalFila2 +=
                        Convert.ToInt32(lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] ==
                                        DBNull.Value
                            ? 0
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)]);
                }
                totalFila2 +=
                    Convert.ToInt32(lista.Tables[1].Rows[0][1] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][1]);
            }
            fila2["Total"] = totalFila2.ToString(CultureInfo.InvariantCulture);
            tabla.Rows.Add(fila2);

            if (totalFila2 == 0)
            {
                tabla.Clear();
            }

            ViewData["listaColumnas"] = listaColumnas;
            ViewData["delegacion"] = delegacion;
            ViewData["fechaCarga"] = fechaCarga;
            ViewData["resFinal"] = resFinal;
            ViewData["horaGestion"] = horaGestion;
            ViewData["despacho"] = despacho;
            return PartialView("GestionXDiaDespacho", tabla);

        }

        public ActionResult GestionXDiaSupervisor(string delegacion, string fechaCarga, string resFinal, string horaGestion, string despacho, string supervisor)
        {
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntGestionMovil().GestionXDiaSupervisor(delegacion ?? "", fechaCarga ?? "", resFinal ?? "", horaGestion ?? "", despacho, supervisor, tipoFormulario??"");
            var columnasCancelar = "";
            var tabla = new DataTable();
            tabla.Columns.Add(new DataColumn { ColumnName = "idUsuario" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Usuario" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Gestor" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Sin fecha de gestion" });

            if (lista.Tables[1].Rows.Count > 0)
            {
                for (var i = 2; i < 34; i++)
                {
                    if (lista.Tables[1].Rows[0][i].ToString() != "0")
                    {
                        var columna = new DataColumn
                        {
                            ColumnName = ("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture))
                        };
                        tabla.Columns.Add(columna);
                        columnasCancelar += ((i - 2) + "|");
                    }
                }
            }
            var columna1 = new DataColumn { ColumnName = "Total" };
            tabla.Columns.Add(columna1);

            var listaColumnas = columnasCancelar.Split('|');
            foreach (DataRow row in lista.Tables[0].Rows)
            {
                var totalFila = 0;
                var fila = tabla.NewRow();
                fila["idUsuario"] = row["idUsuario"];
                fila["Usuario"] = row["Usuario"];
                fila["Gestor"] = row["Nombre"];
                fila["Sin fecha de gestion"] = row["valor32"];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] = row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)];
                    totalFila += Convert.ToInt32(row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)]);
                }
                totalFila += Convert.ToInt32(row["valor32"]);
                fila["Total"] = totalFila.ToString(CultureInfo.InvariantCulture);
                tabla.Rows.Add(fila);

            }

            var totalFila2 = 0;
            var fila2 = tabla.NewRow();
            fila2["idUsuario"] = 0;
            fila2["Usuario"] = "";
            fila2["Gestor"] = "Total";
            if (lista.Tables[1].Rows.Count > 0)
            {
                fila2["Sin fecha de gestion"] = lista.Tables[1].Rows[0][1];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila2["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] =
                        lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value
                            ? "0"
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)];
                    totalFila2 +=
                        Convert.ToInt32(lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] ==
                                        DBNull.Value
                            ? 0
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)]);
                }
                totalFila2 +=
                    Convert.ToInt32(lista.Tables[1].Rows[0][1] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][1]);
            }
            fila2["Total"] = totalFila2.ToString(CultureInfo.InvariantCulture);
            tabla.Rows.Add(fila2);

            if (totalFila2 == 0)
            {
                tabla.Clear();
            }

            ViewData["listaColumnas"] = listaColumnas;
            ViewData["delegacion"] = delegacion;
            ViewData["fechaCarga"] = fechaCarga;
            ViewData["resFinal"] = resFinal;
            ViewData["horaGestion"] = horaGestion;
            ViewData["despacho"] = despacho;
            ViewData["supervisor"] = supervisor;
            return PartialView("GestionXDiaSupervisor", tabla);

        }
        #endregion

        #region Soluciones

        public ActionResult Soluciones(string delegacion = "", string fechaCarga = "", string despacho = "",string supervisor = "")
        {
            if (SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol) == "4")
                return null; //Bloqueo Gestores
            if (SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol) == "5")
                if (new EntRankingIndicadores().ObtenerDelegacionUsuario(
                    SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario))[0] != delegacion)
                    return null; //Bloqueo Delegación
            if (SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol) == "2" ||
                SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol) == "3")
                if (SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio) != despacho)
                    return null; //Bloqueo Despacho y Supervisores
            if (SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol) == "3")
                if (SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario) != supervisor)
                    return null; //Bloqueo Supervisores
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new EntGestionMovil().Soluciones(delegacion ?? "", fechaCarga ?? "", despacho ?? "",
                supervisor ?? "", tipoFormulario??"");
            var columnasCancelar = "";
            var tabla = new DataTable();
            tabla.Columns.Add(new DataColumn {ColumnName = "Estatus final"});
            tabla.Columns.Add(new DataColumn {ColumnName = "Sin resultado final"});

            var titulosTabla = new string[7];
            titulosTabla[0] = "Móvil";
            titulosTabla[1] = "Sin Asignar";
            titulosTabla[2] = "Sin Asignar 2 Visita";
            titulosTabla[3] = "Sin asignar 3 Visita";
            titulosTabla[4] = "Válidas";
            titulosTabla[5] = "Válidas Aprobadas";
            titulosTabla[6] = "Válidas Sin Aprobar";

            if (lista.Tables[1].Rows.Count > 0)
            {
                for (var i = 2; i < 9; i++)
                {
                    if (lista.Tables[1].Rows[0][i].ToString() != "0")
                    {
                        var columna = new DataColumn
                        {
                            ColumnName = ("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture)),
                            DataType = typeof (int)
                        };
                        tabla.Columns.Add(columna);
                        columnasCancelar += ((i - 2) + "|");
                    }
                }
            }
            var columna1 = new DataColumn {ColumnName = "Total", DataType = typeof (int)};
            tabla.Columns.Add(columna1);

            var listaColumnas = columnasCancelar.Split('|');
            foreach (DataRow row in lista.Tables[0].Rows)
            {
                var totalFila = 0;
                var fila = tabla.NewRow();
                fila["Estatus final"] = row["Valor"];
                fila["Sin resultado final"] = row["valor32"];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] =
                        Convert.ToInt32(row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)]);
                    totalFila +=
                        Convert.ToInt32(row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)]);
                }
                totalFila += Convert.ToInt32(row["valor32"]);
                fila["Total"] = totalFila; //.ToString(CultureInfo.InvariantCulture);
                tabla.Rows.Add(fila);

            }

            var dataview = new DataView(tabla) {Sort = "Total ASC"};
            tabla = dataview.ToTable();

            var totalFila2 = 0;
            var fila2 = tabla.NewRow();
            fila2["Estatus final"] = "Total";
            if (lista.Tables[1].Rows.Count > 0)
            {
                fila2["Sin resultado final"] = lista.Tables[1].Rows[0][1];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila2["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] =
                        lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value
                            ? "0"
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)];
                    totalFila2 +=
                        Convert.ToInt32(lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] ==
                                        DBNull.Value
                            ? 0
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)]);
                }
                totalFila2 +=
                    Convert.ToInt32(lista.Tables[1].Rows[0][1] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][1]);
            }
            fila2["Total"] = totalFila2; //.ToString(CultureInfo.InvariantCulture);
            tabla.Rows.Add(fila2);

            if (totalFila2 == 0)
            {
                tabla.Clear();
            }

            ViewData["listaColumnas"] = listaColumnas;
            ViewData["titulosTabla"] = titulosTabla;
            ViewData["delegacion"] = delegacion;
            ViewData["fechaCarga"] = fechaCarga;
            ViewData["despacho"] = despacho;
            ViewData["supervisor"] = supervisor;
            return PartialView("Soluciones", tabla);
        }

        #endregion

        #region GestionXSolucion

        public ActionResult GestionXSolucion(string delegacion="",string fechaCarga="",string estFinal="")
        {
            var idusuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            try
            {
                var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
                var lista = new GestionMovil().GestionXSolucion(delegacion ?? "", fechaCarga ?? "", estFinal ?? "", tipoFormulario ?? "");
                var columnasCancelar = "";
                var tabla = new DataTable();
                tabla.Columns.Add(new DataColumn { ColumnName = "idDespacho" });
                tabla.Columns.Add(new DataColumn { ColumnName = "Despacho" });
                tabla.Columns.Add(new DataColumn { ColumnName = "Sin resultado final" });

                var titulosTabla = new string[7];
                titulosTabla[0] = "Móvil";
                titulosTabla[1] = "Sin Asignar";
                titulosTabla[2] = "Sin Asignar 2 Visita";
                titulosTabla[3] = "Sin asignar 3 Visita";
                titulosTabla[4] = "Válidas";
                titulosTabla[5] = "Válidas Aprobadas";
                titulosTabla[6] = "Válidas Sin Aprobar";

                if (lista.Tables[1].Rows.Count > 0)
                {
                    for (var i = 2; i < 9; i++)
                    {
                        if (lista.Tables[1].Rows[0][i].ToString() != "0")
                        {
                            var columna = new DataColumn
                            {
                                ColumnName = ("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture))
                            };
                            tabla.Columns.Add(columna);
                            columnasCancelar += ((i - 2) + "|");
                        }
                    }
                }
                var columna1 = new DataColumn { ColumnName = "Total" };
                tabla.Columns.Add(columna1);

                var listaColumnas = columnasCancelar.Split('|');
                foreach (DataRow row in lista.Tables[0].Rows)
                {
                    var totalFila = 0;
                    var fila = tabla.NewRow();
                    fila["idDespacho"] = row["idDominio"];
                    fila["Despacho"] = row["NombreDominio"];
                    fila["Sin resultado final"] = row["valor32"];
                    for (var d = 0; d < (listaColumnas.Length - 1); d++)
                    {
                        fila["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] = row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)];
                        totalFila += Convert.ToInt32(row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)]);
                    }
                    totalFila += Convert.ToInt32(row["valor32"]);
                    fila["Total"] = totalFila.ToString(CultureInfo.InvariantCulture);
                    tabla.Rows.Add(fila);

                }

                var totalFila2 = 0;
                var fila2 = tabla.NewRow();
                fila2["idDespacho"] = 0;
                fila2["Despacho"] = "Total";
                if (lista.Tables[1].Rows.Count > 0)
                {
                    fila2["Sin resultado final"] = lista.Tables[1].Rows[0][1];
                    for (var d = 0; d < (listaColumnas.Length - 1); d++)
                    {
                        fila2["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] =
                            lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value
                                ? "0"
                                : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)];
                        totalFila2 +=
                            Convert.ToInt32(lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] ==
                                            DBNull.Value
                                ? 0
                                : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)]);
                    }
                    totalFila2 +=
                        Convert.ToInt32(lista.Tables[1].Rows[0][1] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][1]);
                }
                fila2["Total"] = totalFila2.ToString(CultureInfo.InvariantCulture);
                tabla.Rows.Add(fila2);

                if (totalFila2 == 0)
                {
                    tabla.Clear();
                }

                ViewData["listaColumnas"] = listaColumnas;
                ViewData["titulosTabla"] = titulosTabla;
                ViewData["delegacion"] = delegacion;
                ViewData["fechaCarga"] = fechaCarga;
                ViewData["estFinal"] = estFinal;
                
                return PartialView("GestionXSolucion", tabla);
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, idusuario, "GestionMovilController", string.Format("GestionXSolucion_ delegacion:{0}, fechaCarga:{1}, estFinal:{2}, Error:{3}", delegacion, fechaCarga, estFinal, ex.Message));
                throw;
            }
            
        }

        public ActionResult GestionXSolucionDespacho(string delegacion = "", string fechaCarga = "", string estFinal = "", string despacho = "")
        {
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new GestionMovil().GestionXSolucionDespacho(delegacion ?? "", fechaCarga ?? "", estFinal ?? "", despacho ?? "", tipoFormulario??"");
            var columnasCancelar = "";
            var tabla = new DataTable();
            tabla.Columns.Add(new DataColumn { ColumnName = "idUsuarioPadre" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Usuario" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Nombre" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Sin resultado final" });

            var titulosTabla = new string[7];
            titulosTabla[0] = "Móvil";
            titulosTabla[1] = "Sin Asignar";
            titulosTabla[2] = "Sin Asignar 2 Visita";
            titulosTabla[3] = "Sin asignar 3 Visita";
            titulosTabla[4] = "Válidas";
            titulosTabla[5] = "Válidas Aprobadas";
            titulosTabla[6] = "Válidas Sin Aprobar";

            if (lista.Tables[1].Rows.Count > 0)
            {
                for (var i = 2; i < 9; i++)
                {
                    if (lista.Tables[1].Rows[0][i].ToString() != "0")
                    {
                        var columna = new DataColumn
                        {
                            ColumnName = ("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture))
                        };
                        tabla.Columns.Add(columna);
                        columnasCancelar += ((i - 2) + "|");
                    }
                }
            }
            var columna1 = new DataColumn { ColumnName = "Total" };
            tabla.Columns.Add(columna1);

            var listaColumnas = columnasCancelar.Split('|');
            foreach (DataRow row in lista.Tables[0].Rows)
            {
                var totalFila = 0;
                var fila = tabla.NewRow();
                fila["idUsuarioPadre"] = row["idUsuarioPadre"];
                fila["Usuario"] = row["Usuario"];
                fila["Nombre"] = row["Nombre"];
                fila["Sin resultado final"] = row["valor32"];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] = row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)];
                    totalFila += Convert.ToInt32(row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)]);
                }
                totalFila += Convert.ToInt32(row["valor32"]);
                fila["Total"] = totalFila.ToString(CultureInfo.InvariantCulture);
                tabla.Rows.Add(fila);

            }

            var totalFila2 = 0;
            var fila2 = tabla.NewRow();
            fila2["idUsuarioPadre"] = 0;
            fila2["Usuario"] = "";
            fila2["Nombre"] = "Total";
            if (lista.Tables[1].Rows.Count > 0)
            {
                fila2["Sin resultado final"] = lista.Tables[1].Rows[0][1];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila2["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] =
                        lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value
                            ? "0"
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)];
                    totalFila2 +=
                        Convert.ToInt32(lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] ==
                                        DBNull.Value
                            ? 0
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)]);
                }
                totalFila2 +=
                    Convert.ToInt32(lista.Tables[1].Rows[0][1] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][1]);
            }
            fila2["Total"] = totalFila2.ToString(CultureInfo.InvariantCulture);
            tabla.Rows.Add(fila2);

            if (totalFila2 == 0)
            {
                tabla.Clear();
            }

            ViewData["listaColumnas"] = listaColumnas;
            ViewData["titulosTabla"] = titulosTabla;
            ViewData["delegacion"] = delegacion;
            ViewData["fechaCarga"] = fechaCarga;
            ViewData["estFinal"] = estFinal;
            ViewData["despacho"] = despacho;
            return PartialView("GestionXSolucionDespacho",tabla);
        }

        public ActionResult GestionXSolucionSupervisor(string delegacion = "", string fechaCarga = "", string estFinal = "", string despacho = "", string supervisor = "")
        {
            var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
            var lista = new GestionMovil().GestionXSolucionSupervisor(delegacion ?? "", fechaCarga ?? "", estFinal ?? "", despacho ?? "", supervisor ?? "", tipoFormulario??"");
            var columnasCancelar = "";
            var tabla = new DataTable();
            tabla.Columns.Add(new DataColumn { ColumnName = "idUsuario" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Usuario" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Nombre" });
            tabla.Columns.Add(new DataColumn { ColumnName = "Sin resultado final" });

            var titulosTabla = new string[7];
            titulosTabla[0] = "Móvil";
            titulosTabla[1] = "Sin Asignar";
            titulosTabla[2] = "Sin Asignar 2 Visita";
            titulosTabla[3] = "Sin asignar 3 Visita";
            titulosTabla[4] = "Válidas";
            titulosTabla[5] = "Válidas Aprobadas";
            titulosTabla[6] = "Válidas Sin Aprobar";

            if (lista.Tables[1].Rows.Count > 0)
            {
                for (var i = 2; i < 9; i++)
                {
                    if (lista.Tables[1].Rows[0][i].ToString() != "0")
                    {
                        var columna = new DataColumn
                        {
                            ColumnName = ("Valor" + (i - 2).ToString(CultureInfo.InvariantCulture))
                        };
                        tabla.Columns.Add(columna);
                        columnasCancelar += ((i - 2) + "|");
                    }
                }
            }
            var columna1 = new DataColumn { ColumnName = "Total" };
            tabla.Columns.Add(columna1);

            var listaColumnas = columnasCancelar.Split('|');
            foreach (DataRow row in lista.Tables[0].Rows)
            {
                var totalFila = 0;
                var fila = tabla.NewRow();
                fila["idUsuario"] = row["idUsuario"];
                fila["Usuario"] = row["Usuario"];
                fila["Nombre"] = row["Nombre"];
                fila["Sin resultado final"] = row["valor32"];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] = row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)];
                    totalFila += Convert.ToInt32(row[("valor" + listaColumnas[d]).ToString(CultureInfo.InvariantCulture)]);
                }
                totalFila += Convert.ToInt32(row["valor32"]);
                fila["Total"] = totalFila.ToString(CultureInfo.InvariantCulture);
                tabla.Rows.Add(fila);

            }

            var totalFila2 = 0;
            var fila2 = tabla.NewRow();
            fila2["idUsuario"] = 0;
            fila2["Usuario"] = "";
            fila2["Nombre"] = "Total";
            if (lista.Tables[1].Rows.Count > 0)
            {
                fila2["Sin resultado final"] = lista.Tables[1].Rows[0][1];
                for (var d = 0; d < (listaColumnas.Length - 1); d++)
                {
                    fila2["Valor" + listaColumnas[d].ToString(CultureInfo.InvariantCulture)] =
                        lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] == DBNull.Value
                            ? "0"
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)];
                    totalFila2 +=
                        Convert.ToInt32(lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)] ==
                                        DBNull.Value
                            ? 0
                            : lista.Tables[1].Rows[0][(Convert.ToInt32(listaColumnas[(d)]) + 2)]);
                }
                totalFila2 +=
                    Convert.ToInt32(lista.Tables[1].Rows[0][1] == DBNull.Value ? 0 : lista.Tables[1].Rows[0][1]);
            }
            fila2["Total"] = totalFila2.ToString(CultureInfo.InvariantCulture);
            tabla.Rows.Add(fila2);

            if (totalFila2 == 0)
            {
                tabla.Clear();
            }

            ViewData["listaColumnas"] = listaColumnas;
            ViewData["titulosTabla"] = titulosTabla;
            return PartialView("GestionXSolucionSupervisor", tabla);
        }
        #endregion

        #region GestionXSolucionCC

        public ActionResult GestionXSolucionRDST(string delegacion = "", string fechaCarga = "", string estFinal = "")
        {
            var idusuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            try
            {
                var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
                var lista = new GestionMovil().GestionXSolucionRdst(delegacion ?? "", fechaCarga ?? "", estFinal ?? "", tipoFormulario ?? "");
                
                var tabla = new DataTable();
                tabla.Columns.Add(new DataColumn { ColumnName = "idDominio" });
                tabla.Columns.Add(new DataColumn { ColumnName = "NombreDominio" });
                tabla.Columns.Add(new DataColumn { ColumnName = "SinGestionar" });
                tabla.Columns.Add(new DataColumn { ColumnName = "Gestionadas" });
                tabla.Columns.Add(new DataColumn { ColumnName = "Total" });

                //var titulosTabla = new string[2];
                //titulosTabla[0] = "Sin Gestionar";
                //titulosTabla[1] = "Gestionadas";

                foreach (DataRow row in lista.Tables[0].Rows)
                {
                    var fila = tabla.NewRow();
                    fila["idDominio"] = row["idDominio"];
                    fila["NombreDominio"] = row["NombreDominio"];
                    fila["SinGestionar"] = row["SinGestionar"];
                    fila["Gestionadas"] = row["Gestionadas"];
                    fila["Total"] = row["Total"];
                    tabla.Rows.Add(fila);

                }

                //ViewData["titulosTabla"] = titulosTabla;
                ViewData["delegacion"] = delegacion;
                ViewData["fechaCarga"] = fechaCarga;
                ViewData["estFinal"] = estFinal;
                return PartialView("GestionXSolucionRDST", tabla);
                
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, idusuario, "GestionMovilController", string.Format("GestionXSolucion_ delegacion:{0}, fechaCarga:{1}, estFinal:{2}, Error:{3}", delegacion, fechaCarga, estFinal, ex.Message));
                throw;
            }

        }

        public ActionResult GestionXSolucionRDSTSupervisor(string delegacion = "", string fechaCarga = "", string estFinal = "", string despacho = "", string supervisor = "")
        {
            var idusuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            try
            {
                var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
                var lista = new GestionMovil().GestionXSolucionSupervisorRdst(delegacion ?? "", fechaCarga ?? "", estFinal ?? "", despacho ?? "", tipoFormulario ?? "");
                
                var tabla = new DataTable();
                tabla.Columns.Add(new DataColumn { ColumnName = "idusuario" });
                tabla.Columns.Add(new DataColumn { ColumnName = "Usuario" });
                tabla.Columns.Add(new DataColumn { ColumnName = "Nombre" });
                tabla.Columns.Add(new DataColumn { ColumnName = "SinGestionar" });
                tabla.Columns.Add(new DataColumn { ColumnName = "Gestionadas" });

               

                foreach (DataRow row in lista.Tables[0].Rows)
                {
                    var fila = tabla.NewRow();
                    fila["idusuario"] = row["idusuario"];
                    fila["Usuario"] = row["Usuario"];
                    fila["Nombre"] = row["Nombre"];
                    fila["SinGestionar"] = row["SinGestionar"];
                    fila["Gestionadas"] = row["Gestionadas"];
                    tabla.Rows.Add(fila);

                }

                //ViewData["titulosTabla"] = titulosTabla;
                ViewData["delegacion"] = delegacion;
                ViewData["fechaCarga"] = fechaCarga;
                ViewData["estFinal"] = estFinal;
                return PartialView("GestionXSolucionSupervisorRDST", tabla);
                
            }
            catch (Exception ex)
            {

                Logger.WriteLine(Logger.TipoTraceLog.Error, idusuario, "GestionMovilController", string.Format("GestionXSolucion_ delegacion:{0}, fechaCarga:{1}, estFinal:{2}, Error:{3}", delegacion, fechaCarga, estFinal, ex.Message));
                throw;
            }

        }
        
        #endregion

        #region Pagos

        public ActionResult ReportePagos(string despacho="" )
        {
           var idusuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
           var idRol = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol);

           despacho = string.IsNullOrEmpty(despacho) ? "0,1".Contains(idRol) ? "0" : SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio) : despacho;
            try
            {
                var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
                var ds = new GestionMovil().ReporteGestionMovil_PagosDespachos(Convert.ToInt32(despacho), tipoFormulario);
                
                var tabla = new DataTable();
                tabla.Columns.Add(new DataColumn { ColumnName = "idDominio" });
                tabla.Columns.Add(new DataColumn { ColumnName = "NombreDominio" });
                tabla.Columns.Add(new DataColumn { ColumnName = "SinPago" });
                tabla.Columns.Add(new DataColumn { ColumnName = "PagoParcial" });
                tabla.Columns.Add(new DataColumn { ColumnName = "PagoTotal" });
                tabla.Columns.Add(new DataColumn { ColumnName = "Total" });


                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var fila = tabla.NewRow();
                    fila["idDominio"] = row["iddominio"];
                    fila["NombreDominio"] = row["NombreDominio"];
                    fila["SinPago"] = row["SinPago"];
                    fila["PagoParcial"] = row["PagoParcial"];
                    fila["PagoTotal"] = row["PagoTotal"];
                    fila["Total"] = row["Total"];
                    tabla.Rows.Add(fila);

                }
                ViewData["despacho"] = despacho;
                ViewData["usuario"] = idusuario;
                return PartialView(tabla);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idusuario, "GestionMovilController", string.Format("ReportePagos despacho:{0} Error:{1}", despacho, ex.Message));
                throw;
            }

        }


        public ActionResult ReportePagosDelegaciones(string despacho = "")
        {
           var idusuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
           var idRol = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol);

           despacho = string.IsNullOrEmpty(despacho) ? "0,1".Contains(idRol) ? "0" : SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio) : despacho;
            try
            {
                var tipoFormulario = CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta;
                var ds = new GestionMovil().ReporteGestionMovil_PagosDelagaciones(Convert.ToInt32(despacho), tipoFormulario,"");
                
                var tabla = new DataTable();
                tabla.Columns.Add(new DataColumn { ColumnName = "idDominio" });
                tabla.Columns.Add(new DataColumn { ColumnName = "Delegacion" });
                tabla.Columns.Add(new DataColumn { ColumnName = "SinPago" });
                tabla.Columns.Add(new DataColumn { ColumnName = "PagoParcial" });
                tabla.Columns.Add(new DataColumn { ColumnName = "PagoTotal" });
                tabla.Columns.Add(new DataColumn { ColumnName = "Total" });


                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var fila = tabla.NewRow();
                    fila["idDominio"] = row["iddominio"];
                    fila["Delegacion"] = row["Delegacion"];
                    fila["SinPago"] = row["SinPago"];
                    fila["PagoParcial"] = row["PagoParcial"];
                    fila["PagoTotal"] = row["PagoTotal"];
                    fila["Total"] = row["Total"];
                    tabla.Rows.Add(fila);

                }
                ViewData["despacho"] = despacho;
                ViewData["usuario"] = idusuario;
                ViewData["Delegacion"] = idusuario;
                
                return PartialView(tabla);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idusuario, "GestionMovilController", string.Format("ReportePagos despacho:{0} Error:{1}", despacho, ex.Message));
                throw;
            }

        }

        
        #endregion

        #region MarcajeNoExitoso

        public ActionResult MarcajeNoExitoso()
        {
            var idusuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            var idDominio = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio);
            var idRol = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol);

            
            try
            {
               
                var ds = new GestionMovil().ReporteGestionMovil_LlamadasNoExito(1,-1);


                var tabla = new DataTable();
                int i = 1;
                foreach (var col in ds.Tables[0].Columns)
                {
                    tabla.Columns.Add(new DataColumn { ColumnName = col.ToString() });
                    i++;
                }

                var totalGeneral = new int[i];
                
                
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var fila = tabla.NewRow();
                    fila["idDominio"] = row["iddominio"];
                    fila["NombreDominio"] = row["NombreDominio"];

                    for (int j = 2; j < i-1; j++)
                    {
                        fila[j] = String.IsNullOrEmpty(row[j].ToString()) ? "0" : row[j];
                        totalGeneral[j] += Convert.ToInt32(fila[j]);
                    }

                    fila["Total"] = row["Total"];
                    tabla.Rows.Add(fila);

                }

                var filaTotal = tabla.NewRow();
                filaTotal["idDominio"] = "-1";
                filaTotal["NombreDominio"] ="Total" ;

                for (int j = 2; j < totalGeneral.Length - 2; j++)
                {
                    filaTotal[j] =  totalGeneral[j];
                    totalGeneral[j] += Convert.ToInt32(totalGeneral[j]);
                }

                filaTotal["Total"] = totalGeneral[totalGeneral.Length-2];
                tabla.Rows.Add(filaTotal);
                ViewData["despacho"] = idDominio;
                ViewData["usuario"] = idusuario;
                ViewData["dias"] = i-4;
                
                return PartialView(tabla);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idusuario, "GestionMovilController", string.Format("ReportePagos despacho:{0} Error:{1}", idDominio, ex.Message));
                throw;
            }
        }

        public ActionResult MarcajeNoExitosoDelegaciones(string despacho = "")
        {
            var idusuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            var idDominio = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio);

            try
            {

                var ds = new GestionMovil().ReporteGestionMovil_LlamadasNoExito(2, Convert.ToInt32(despacho));


                var tabla = new DataTable();
                int i = 1;
                foreach (var col in ds.Tables[0].Columns)
                {
                    tabla.Columns.Add(new DataColumn { ColumnName = col.ToString() });
                    i++;
                }

                var totalGeneral = new int[i];


                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var fila = tabla.NewRow();
                    fila["idDominio"] = row["iddominio"];
                    
                    fila["Delegacion"] = row["Delegacion"];
                    

                    for (int j = 2; j < i - 1; j++)
                    {
                        fila[j] = String.IsNullOrEmpty(row[j].ToString()) ? "0" : row[j];
                        totalGeneral[j] += Convert.ToInt32(fila[j]);
                    }

                    fila["Total"] = row["Total"];
                    tabla.Rows.Add(fila);

                }

                var filaTotal = tabla.NewRow();
                filaTotal["idDominio"] = "-1";
                filaTotal["Delegacion"] = "Total";

                for (int j = 2; j < totalGeneral.Length - 2; j++)
                {
                    filaTotal[j] = totalGeneral[j];
                    totalGeneral[j] += Convert.ToInt32(totalGeneral[j]);
                }

                filaTotal["Total"] = totalGeneral[totalGeneral.Length - 2];
                tabla.Rows.Add(filaTotal);
                ViewData["despacho"] = despacho;
                ViewData["usuario"] = idusuario;
                ViewData["dias"] = i - 4;

                return PartialView(tabla);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idusuario, "GestionMovilController", string.Format("ReportePagos despacho:{0} Error:{1}", idDominio, ex.Message));
                throw;
            }
        }
        #endregion
    }
}

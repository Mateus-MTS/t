using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedagio
{
    class Tarifas
    {
        #region PROPRIEDADES

        private double _Tarifa = 7.90;
        private byte _AtivaDesconto = 0;
        private int _NrVezesNoMes = 1;
        private double _DescontoTarifa = 5.0;
        private double _ValorTotalPorPassagem = 7.90;
        public double Tarifa
        {
            get
            {
                return this._Tarifa;
            }
            private set
            {
                this._Tarifa = value;
            }
        }
        public byte AtivaDesconto
        {
            get
            {
                return this._AtivaDesconto;
            }
            private set
            {
                this._AtivaDesconto = value;
            }
        }
        public int NrVezesNoMes
        {
            get
            {
                return this._NrVezesNoMes;
            }
            set
            {
                this._NrVezesNoMes = value;
            }
        }
        public double DescontoTarifa
        {
            get
            {
                return this._DescontoTarifa;
            }
            private set
            {
                this._DescontoTarifa = value;
            }
        }
        public double ValorTotalPorPassagem
        {
            get
            {
                return this._ValorTotalPorPassagem;
            }
            set
            {
                this._ValorTotalPorPassagem = value;
            }
        }
        public int Id_Veiculo { get; set; }
        public string Veiculo { get; set; }
        public string Placa { get; set; }
        public string Mes { get; set; }
        public DateTime DataPassagem { get; set; }

        #endregion


        Dados objAcessoBanco = new Dados();

        public void Gravar(Tarifas obj_Tarifas)
        {

            double tarifaMinima = Tarifa * 20 / 100;
            tarifaMinima = Convert.ToDouble(tarifaMinima.ToString("F2"));
            DataTable dt = TotalizaNrVezesNoMes(obj_Tarifas.Id_Veiculo, obj_Tarifas.Placa);

            if (dt.Rows.Count == 0 || dt.Rows == null)
            {
                AtivaDesconto = 0;
                NrVezesNoMes = 1;
                DescontoTarifa = 0;
                ValorTotalPorPassagem = Tarifa;
                DataPassagem = DateTime.Now;
            }
            else
            {
                foreach (DataRow r in dt.Rows)
                {
                    int nrVezesNoMes = Convert.ToInt32(r["NrVezesNoMes"].ToString());
                    NrVezesNoMes = nrVezesNoMes + 1;


                    if (NrVezesNoMes >= 10)
                    {
                        AtivaDesconto = 1;
                        Tarifa = Convert.ToDouble(r["ValorTotalPorPassagem"].ToString());
                        DescontoTarifa = Tarifa * DescontoTarifa / 100;
                        ValorTotalPorPassagem = Tarifa - DescontoTarifa;
                        ValorTotalPorPassagem = Convert.ToDouble(ValorTotalPorPassagem.ToString("F2"));
                        DataPassagem = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"));

                        if (ValorTotalPorPassagem < tarifaMinima)
                        {
                            ValorTotalPorPassagem = tarifaMinima;
                        }
                    }
                    else
                    {
                        AtivaDesconto = 0;
                        DescontoTarifa = 0;
                        ValorTotalPorPassagem = Convert.ToDouble(Tarifa.ToString("F2"));
                        DataPassagem = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy"));
                    }
                }
            }

            objAcessoBanco.GravarTarifaPassagem(Tarifa,
                                                obj_Tarifas.Id_Veiculo,
                                                obj_Tarifas.Veiculo,
                                                obj_Tarifas.Placa,
                                                obj_Tarifas.Mes,
                                                AtivaDesconto,
                                                NrVezesNoMes,
                                                DescontoTarifa,
                                                ValorTotalPorPassagem,
                                                DataPassagem);
        }

        public DataTable TotalizaNrVezesNoMes(int idVeiculo, string placa)
        {
            return objAcessoBanco.TotalizaPassagens(idVeiculo, placa);
        }
    }
}

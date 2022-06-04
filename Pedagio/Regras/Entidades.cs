using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedagio
{
    class Entidades
    {
        private int _Cod_Proprietario = 0;
        private string _Proprietario;
        private int _Id_Veiculo = 0;
        private string _Veiculo;
        private string _Placa = "";

        public int Cod_Proprietario
        {
            get
            {
                return this._Cod_Proprietario;
            }
            private set
            {
                this._Cod_Proprietario = value;
            }
        }
        public string Proprietario
        {
            get
            {
                return this._Proprietario;
            }
            set
            {
                this._Proprietario = value;
            }
        }
        public int Id_Veiculo
        {
            get
            {
                return this._Id_Veiculo;
            }
            private set
            {
                this._Id_Veiculo = value;
            }
        }
        public string Veiculo
        {
            get
            {
                return this._Veiculo;
            }
            set
            {
                this._Veiculo = value;
            }
        }
        public string Placa
        {
            get
            {
                return this._Placa;
            }
            set
            {
                this._Placa = value;
            }
        }

        DadosEntidade objAcessoBanco = new DadosEntidade();
        DadosVeiculo objAcessoVeiculo = new DadosVeiculo();
        Tarifas obj_tarifas = new Tarifas();

        public void Gravar(Entidades obj_Entidade)
        {

            DataTable dtCodProprietario = objAcessoBanco.RetornarCodMaxProprietario(obj_Entidade.Placa);
            if (dtCodProprietario.Rows.Count > 1 && dtCodProprietario.Rows == null)
            {
                foreach (DataRow r in dtCodProprietario.Rows)
                {
                    Cod_Proprietario = Convert.ToInt32(r["ID"].ToString());
                }
            }
            else
            {
                Cod_Proprietario++;
            }

            DataTable dtIdVeiculo = objAcessoBanco.RetornarIdMaxVeiculo(obj_Entidade.Placa);
            if (dtIdVeiculo.Rows.Count > 0 || dtIdVeiculo.Rows != null)
            {
                foreach (DataRow r in dtIdVeiculo.Rows)
                {
                    Id_Veiculo = Convert.ToInt32(r["ID"].ToString());
                }
            }
            else
            {
                this.Id_Veiculo++;
            }

            if (objAcessoBanco.RetornaUsuarios(Cod_Proprietario, Id_Veiculo, obj_Entidade.Placa) == false)
            {
                if (objAcessoBanco.Cadastrar(Cod_Proprietario,
                                             obj_Entidade.Proprietario,
                                             Id_Veiculo,
                                             obj_Entidade.Veiculo,
                                             obj_Entidade.Placa) == true)
                {
                    obj_tarifas.Id_Veiculo = Id_Veiculo;
                    obj_tarifas.Veiculo = Veiculo;
                    obj_tarifas.Placa = Placa;
                    obj_tarifas.Mes = DateTime.Now.ToString("MMM");
                    obj_tarifas.Gravar(obj_tarifas);
                }
                else
                    Console.WriteLine("Não foi possível gravar os dados do proprietário!");
            }
            else
            {
                obj_tarifas.Id_Veiculo = Id_Veiculo;
                obj_tarifas.Veiculo = Veiculo;
                obj_tarifas.Placa = Placa;
                obj_tarifas.Mes = DateTime.Now.ToString("MMM");

            }
        
        }

    }
}

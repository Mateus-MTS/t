using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Dados.Pedagio
{
    class DadosEntidade : AcessoBD
    {
        public bool Cadastrar(int Cod_Proprietario,
                              string Proprietario,
                              int Id_Veiculo,
                              string Veiculo,
                              string Placa)
        {
            string COMANDO = string.Concat("INSERT INTO Entidade ",
                                            "VALUES ",
                                            "( ",
                                            "@Cod_Proprietario, ",
                                            "@Proprietario, ",
                                            "@Id_Veiculo, ",
                                            "@Veiculo, ",
                                            "@Placa ",
                                            ") ");

            Cmd = new SqlCommand(COMANDO, Cnx);
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandTimeout = this.TempoDoTimeOut;
            {
                var withBlock = Cmd.Parameters;
                withBlock.Add("@Cod_Proprietario", SqlDbType.Int).Value = Cod_Proprietario;
                withBlock.Add("@Proprietario", SqlDbType.VarChar, 30).Value = Proprietario;
                withBlock.Add("@Id_Veiculo", SqlDbType.Int).Value = Id_Veiculo;
                withBlock.Add("@Veiculo", SqlDbType.VarChar, 10).Value = Veiculo;
                withBlock.Add("@Placa", SqlDbType.VarChar, 7).Value = Placa;
            }
            try
            {
                Cnx.Open();
                Cmd.ExecuteNonQuery();
                Cnx.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Cnx.Close();
                return false;
            }
        }

        public bool GravarTarifaPassagem(double tarifa,
                                         int Id_Veiculo,
                                         string Veiculo,
                                         string Placa,
                                         string Mes,
                                         byte AtivaDesconto,
                                         int NrVezesNoMes,
                                         double DescontoTarifa,
                                         double ValorTotalPorPassagem,
                                         DateTime DataPassagem)
        {
            string COMANDO = string.Concat("INSERT INTO TarifaPassagem ",
                                            "VALUES ",
                                            "(@Id_Veiculo, ",
                                            "@Veiculo, ",
                                            "@Placa, ",
                                            "@tarifa, ",
                                            "@Mes, ",
                                            "@AtivaDesconto, ",
                                            "@NrVezesNoMes, ",
                                            "@DescontoTarifa, ",
                                            "@ValorTotalPorPassagem, ",
                                            "@DataPassagem ",
                                            ") ");

            Cmd = new SqlCommand(COMANDO, Cnx);
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandTimeout = this.TempoDoTimeOut;
            {
                var withBlock = Cmd.Parameters;
                withBlock.Add("@Id_Veiculo", SqlDbType.Int).Value = Id_Veiculo;
                withBlock.Add("@Veiculo", SqlDbType.VarChar, 10).Value = Veiculo;
                withBlock.Add("@Placa", SqlDbType.VarChar, 7).Value = Placa;
                withBlock.Add("@tarifa", SqlDbType.Float).Value = tarifa;
                withBlock.Add("@Mes", SqlDbType.VarChar, 10).Value = Mes;
                withBlock.Add("@AtivaDesconto", SqlDbType.TinyInt).Value = AtivaDesconto;
                withBlock.Add("@NrVezesNoMes", SqlDbType.Int).Value = NrVezesNoMes;
                withBlock.Add("@DescontoTarifa", SqlDbType.Float).Value = DescontoTarifa;
                withBlock.Add("@ValorTotalPorPassagem", SqlDbType.Float).Value = ValorTotalPorPassagem;
                withBlock.Add("@DataPassagem", SqlDbType.Date).Value = DataPassagem;
            }
            try
            {
                Cnx.Open();
                Cmd.ExecuteNonQuery();
                Cnx.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Cnx.Close();
                return false;
            }
        }


        public DataTable RetornarCodMaxProprietario(string placa)
        {

            string Comando = string.Concat("SELECT MAX(Cod_Proprietario) as ID FROM Entidade ",
                                           "WHERE Placa = @placa ");

            Cmd = new SqlCommand(Comando, Cnx);
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandTimeout = this.TempoDoTimeOut;
            {
                var withBlock = Cmd.Parameters;
                withBlock.Add("@Placa", SqlDbType.VarChar, 7).Value = placa;
            }
            var Da = new SqlDataAdapter(Cmd);
            var Dt = new DataTable();
            Dt.Clear();
            try
            {
                Cnx.Open();
                Da.Fill(Dt);
                Cnx.Close();
                return Dt;
            }
            catch (Exception ex)
            {
                Cnx.Close();
                Interaction.MsgBox(ex.ToString());
            }
            return Dt;
        }

        public DataTable RetornarIdMaxVeiculo(string placa)
        {

            string Comando = string.Concat("SELECT MAX(Id_Veiculo) as ID FROM Entidade ",
                                           "WHERE Placa = @placa ");

            Cmd = new SqlCommand(Comando, Cnx);
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandTimeout = this.TempoDoTimeOut;
            {
                var withBlock = Cmd.Parameters;
                withBlock.Add("@Placa", SqlDbType.VarChar, 7).Value = placa;
            }
            var Da = new SqlDataAdapter(Cmd);
            var Dt = new DataTable();
            Dt.Clear();
            try
            {
                Cnx.Open();
                Da.Fill(Dt);
                Cnx.Close();

                if (Dt.Rows.Equals(DBNull.Value) && Dt.Rows.Count==1)
                {
                    foreach (DataRow r in Dt.Rows)
                        Dt.Rows.Remove(r);
                }
                return Dt;
            }
            catch (Exception ex)
            {
                Cnx.Close();
                Console.WriteLine(ex.ToString());
            }
            return Dt;
        }

        public DataTable TotalizaPassagens(int idVeiculo, string placa)
        {

            string Comando = String.Concat("SELECT Id_Veiculo, ",
                                            "Veiculo, ",
                                            "Placa, ",
                                            "tarifa, ",
                                            "Mes, ",
                                            "AtivaDesconto, ",
                                            "NrVezesNoMes, ",
                                            "DescontoTarifa, ",
                                            "ValorTotalPorPassagem, ",
                                            "DataPassagem, ",
                                            "DATEADD(DAY, +1, EOMONTH(GETDATE(), -1)) as PrimeiroDiaMes, ",
                                            "EOMONTH(GETDATE()) As UltimoDiaMes ",
                                            "FROM TarifaPassagem ",
                                            "WHERE Id_Veiculo = @Id_Veiculo ",
                                            "AND Placa = @Placa ");

            Cmd = new SqlCommand(Comando, Cnx);
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandTimeout = this.TempoDoTimeOut;
            {
                var withBlock = Cmd.Parameters;
                withBlock.Add("@Id_Veiculo", SqlDbType.Int).Value = idVeiculo;
                withBlock.Add("@Placa", SqlDbType.VarChar, 7).Value = placa;
            }
            var Da = new SqlDataAdapter(Cmd);
            var Dt = new DataTable();
            Dt.Clear();
            try
            {
                Cnx.Open();
                Da.Fill(Dt);
                Cnx.Close();
                return Dt;
            }
            catch (Exception ex)
            {
                Cnx.Close();
                Console.WriteLine(ex.ToString());
            }
            return Dt;
        }

        public bool RetornaUsuarios(int codProprietario, int idVeiculo, string placa)
        {

            string Comando = String.Concat("SELECT * FROM Entidade ",
                                            "WHERE Id_Veiculo = @Id_Veiculo ",
                                            "AND Cod_Proprietario = @Cod_Proprietario ",
                                            "AND Placa = @Placa ");

            Cmd = new SqlCommand(Comando, Cnx);
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandTimeout = this.TempoDoTimeOut;
            {
                var withBlock = Cmd.Parameters;
                withBlock.Add("@Id_Veiculo", SqlDbType.Int).Value = idVeiculo;
                withBlock.Add("@Cod_Proprietario", SqlDbType.Int).Value = codProprietario;
                withBlock.Add("@Placa", SqlDbType.VarChar, 7).Value = placa;
            }
            var Da = new SqlDataAdapter(Cmd);
            var Dt = new DataTable();
            Dt.Clear();
            try
            {
                Cnx.Open();
                Da.Fill(Dt);
                Cnx.Close();
                return true;
            }
            catch (Exception ex)
            {
                Cnx.Close();
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}

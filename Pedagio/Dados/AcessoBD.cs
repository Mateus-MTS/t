using System;
using System.Data.SqlClient;

namespace Dados.Pedagio
{
    class AcessoBD
    {
        private string strConexao;
        public SqlCommand Cmd;

        private SqlConnection m_cnx = null;

        public SqlConnection Cnx
        {
            get
            {
                try
                {
                    try
                    {
                        strConexao = @"Server=SIRIUS\SQLEXPRESS;Database=Pedagio;Integrated Security=true";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                if (strConexao == null)
                    strConexao = @"Server=SIRIUS\SQLEXPRESS;Database=Pedagio;Integrated Security=true";


                if ((this.m_cnx == null))
                    this.m_cnx = new SqlConnection(strConexao);

                return m_cnx;
            }

            set
            {
                m_cnx = value;
            }
        }

        /* TempoDoTimeOut
           consiste em um tempo limite (geralmente, medido em segundos) em que uma operação irá 
           aguardar até que ela encerre a execução de forma forçada, caso esse tempo limite seja atingido.
        */
        public int TempoDoTimeOut
        {
            get
            {
                return 600;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.Web;
using Tools;


namespace DataAccess.SqlServer.Extend
{
    /// <summary>
    /// SQL的数据库连接方式
    /// </summary>
    public enum DBSQLTYPE {WIN,WEB};
   
    /// <summary>
    /// Name:SQL Server数据库访问类
    /// Description:
    /// 重要说明：
    /// 由于该类采用所有的数据库命令都是存储过程，所以请在使用该类前确保您的数据库有如下格式的存储过程，
    /// 定义格式如下：
    /// /*
    ///通用存储过程命令
    ///*/
    ///create proc [dbo].[upCommand]
    ///@cmd ntext
    ///as 
    ///exec (@cmd)
    /// 如果是Web应用程序，请在web.config中设置的appSettings的数据库连接字符串，变量名为：connectionStrings
    /// </summary>
    public class SqlHeler 
    {
        #region var
        private string strConnect = "";
        private string strService = "";  // 定义服务器名称
        private string strDataBase = ""; //定义数据库名称
        private string strUser = "";  //定义登陆用户名
        private string strPassword = ""; //定义登陆用户密码
        private int intType = 0;  //定义连接类型  
        private DBSQLTYPE dbConnectType = DBSQLTYPE.WEB ;//数据库类别，是web还是win
        private SqlConnection sqlConnect = new SqlConnection();
        #endregion

        #region property
        /// <summary>
        /// 连接服务器名称
        /// </summary>
        public string serviceName
        {
            get { return strService; }
            set { strService = value; }
        }
        /// <summary>
        /// 连接数据库名称
        /// </summary>
        public string databaseName
        {
            get { return strDataBase; }
            set { strDataBase = value; }
        }
        /// <summary>
        /// 连接数据库用户名称
        /// </summary>
        public string userName
        {
            get { return strUser; }
            set { strUser = value; }
        }
        /// <summary>
        /// 连接数据库密码
        /// </summary>
        public string password
        {
            get { return strPassword; }
            set { strPassword = value; }
        }
        /// <summary>
        /// 连接数据库类型
        /// </summary>
        public int type
        {
            get { return intType; }
            set { intType = value; }
        }
        /// <summary>
        /// 设置或获取连接字符串
        /// </summary>
        public string connection
        {
            get { return strConnect; }
            set { strConnect = value; }
        }
        /// <summary>
        /// 设置或获取web还是win
        /// </summary>
        public DBSQLTYPE connectType
        {
            get { return dbConnectType; }
            set { dbConnectType=value; }
        }
        #endregion

        #region method    
        /// <summary>
        /// 构造方法
        /// </summary>      
        public SqlHeler()
        {
            if (dbConnectType == DBSQLTYPE.WIN)
            {
                try
                {
                    DBConfig con = new DBConfig();
                    con = con.getConfig();
                    serviceName = con.serviceName;
                    databaseName = con.databaseName;
                    userName = con.userName;
                    password = con.password;
                    type = con.type;
                }
                catch (Exception)
                {
                    
                }    
            }
            else
                strConnect = Tools.ConfigManager.ReadConnectionStringByName(ConfigurationFile.WebConfig,
                                "connectionStrings");
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbType">连接类别，是Web还是Win方式，默认Web</param>
        public SqlHeler(DBSQLTYPE dbType)
        {
            if (dbType == DBSQLTYPE.WIN)
            {
                try
                {
                    DBConfig con = new DBConfig();
                    con = con.getConfig();
                    serviceName = con.serviceName;
                    databaseName = con.databaseName;
                    userName = con.userName;
                    password = con.password;
                    type = con.type;
                }
                catch (Exception)
                {
                }
            }
            else
                strConnect = Tools.ConfigManager.ReadConnectionStringByName(ConfigurationFile.WebConfig,
                    "connectionStrings");
        }        

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <returns>返回连接对象</returns>
        public SqlConnection getConnection()
        {
            try
            {
                if (dbConnectType == DBSQLTYPE.WEB)
                {
                    sqlConnect.ConnectionString = strConnect;
                    return sqlConnect;
                }
                else
                {
                    try
                    {
                        switch (type)
                        {
                            case 0: //远程连接
                                strConnect = "Data Source=" + serviceName + ",1433;Network Library=DBMSSOCN;Initial Catalog=" + databaseName + ";User Id=" + userName + ";password=" + password + ";Connect Timeout=60;";
                                break;
                            case 1://本地连接
                                strConnect = "workstation id=" + serviceName + ";packet size=4096;integrated security=SSPI;data source=" + serviceName + ";persist security info=False;initial catalog=" + databaseName + ";Connect Timeout=60;";
                                break;
                            case 2: // 信用连接
                                strConnect = "Server=" + serviceName + ";Database=" + databaseName + ";User ID=" + userName + ";Password=" + password + ";Trusted_Connection=False" + ";Connect Timeout=60;";
                                break;
                            case 3: //默认连接
                                strConnect = "Data Source=" + serviceName + ";User ID=" + userName + ";Pwd=" + password + ";Initial Catalog=" + databaseName + ";Connect Timeout=60;";
                                break;
                            default:  // 本地连接
                                //strConnect = "workstation id=" + serviceName + ";packet size=4096;integrated security=SSPI;data source=" + serviceName  + ";persist security info=False;initial catalog=" + databaseName + ";Connect Timeout=60;";
                                break;
                        }
                        sqlConnect.ConnectionString = strConnect;                       
                        return sqlConnect;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                sqlConnect.Close();
            }
        }
        /// <summary>
        /// //测试连接的方法.
        /// </summary>
        /// <returns>返回值如果为true表示测试连接成功，否则表示失败</returns>
        public  bool testConnect()
        {
            try
            {                
                sqlConnect.ConnectionString = strConnect;
                sqlConnect.Open();
                if (sqlConnect.State == ConnectionState.Open)
                {                    
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception)
            {                
                return false;
            }
            finally
            { 
            sqlConnect.Close();
            }
        }
       
        /// <summary>
        /// 数据库操作命令。通过调用数据库中的存储过程upCommand完成，
        /// upCommand的存储过程定义 格式如下：
        /// create proc [dbo].[upCommand]
        ///@cmd ntext
        ///as 
        ///exec (@cmd)
        /// </summary>
        /// <param name="cmd">数据库操作命令</param>
        /// <returns>返回值大于0表示操作成功，否则表示失败</returns>
        public int sqlCommand(string cmd) 
        {
            try
            {               
                sqlConnect = getConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConnect;
                if (sqlConnect!=null) //数据库连接创建成功
                {
                    if (sqlConnect.State!=ConnectionState.Open)
                    sqlConnect.Open();
                    /*
                     存储过程设置
                     */
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "upCommand";
                    sqlCmd.Parameters.AddWithValue("@cmd", cmd);
                    //sqlCmd.CommandText = cmd; 不依赖于数据库中的存储过程使用的常规命令                  
                    return sqlCmd.ExecuteNonQuery();                                 
                }
                else
                    return 0; //数据库连接创建失败
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                sqlConnect.Close();
            }
        }
        /// <summary>
        /// 数据库操作命令。支持存储过程，如果参数isProc
        /// 为true,表示使用存储过程，需要数据库中有存储过
        /// 程upCommand支持，upCommand的存储过程定义 格式如下：
        /// create proc [dbo].[upCommand]
        ///@cmd ntext
        ///as 
        ///exec (@cmd)；如果没有，请将参数isProc设置为false即可。
        /// </summary>
        /// <param name="cmd">数据库操作命令</param>
        /// <param name="isProc">是否有存储过程，true表示有，false表示没有</param>
        /// <returns>返回值大于0表示操作成功，否则表示失败</returns>
        public int sqlCommand(string cmd,bool isProc)
        {
            try
            {
                sqlConnect = getConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConnect;
                if (sqlConnect != null) //数据库连接创建成功
                {
                    if (sqlConnect.State != ConnectionState.Open)
                        sqlConnect.Open();
                    if (isProc)
                    {
                        /*
                         存储过程设置
                         */
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "upCommand";
                        sqlCmd.Parameters.AddWithValue("@cmd", cmd);
                    }
                    else
                    {
                        sqlCmd.CommandType = CommandType.Text;
                         sqlCmd.CommandText = cmd; //不依赖于数据库中的存储过程使用的常规命令                  
   
                    }
                     return sqlCmd.ExecuteNonQuery();
                }
                else
                    return 0; //数据库连接创建失败
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                sqlConnect.Close();
            }
        }
        /// <summary>
        /// 多个数据库操作命令。
        /// </summary>
        /// <param name="strcmd">数据库操作命令数组</param>
        /// <returns>返回值大于0表示操作成功，否则表示失败</returns>
        public int sqlCommand(string[] strcmd)
        {
            try
            {
                sqlConnect = getConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConnect;
                if (sqlConnect != null) //数据库连接成功
                {
                    if (sqlConnect.State != ConnectionState.Open)
                        sqlConnect.Open();
                    if (sqlConnect.State == ConnectionState.Open)
                    {
                        SqlTransaction trans; //定义事务变量
                        trans = sqlConnect.BeginTransaction(); //开始事务                   
                        int i;
                        //循环执行数组命令
                        for (i = 0; i < strcmd.Length; i++)
                        {

                            sqlCmd.Connection = sqlConnect;
                            sqlCmd.CommandTimeout = 0;
                            /*
                            //存储过程设置，Version2.0.0.0
                            //*/
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.CommandText = "upCommand";
                            sqlCmd.Parameters.Clear();
                            sqlCmd.Parameters.AddWithValue("@cmd", strcmd[i]);
                            /*
                             Version1.0.0.0
                             **/
                            //sqlCmd.CommandType = CommandType.Text;//字符串命令类型
                            //sqlCmd.CommandText = strcmd[i];                         
                            sqlCmd.Transaction = trans; //命令获取事务 
                            int state = sqlCmd.ExecuteNonQuery();
                            if (state < 0)
                            {
                                //执行失败，事务回滚。
                                trans.Rollback();
                                return 0;
                            }
                        }
                        trans.Commit(); //执行事务;事务结束点。能执行则成功
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                sqlConnect.Close();
            }
        }
       
        
        /// <summary>
        /// 多个数据库操作命令。
        /// </summary>
        /// <param name="strcmd">数据库操作命令数组</param>
        /// <param name="isProc">是否有存储过程，true表示有，false表示没有</param>
        /// <returns>返回值大于0表示操作成功，否则表示失败</returns>
        public int sqlCommand(string[] strcmd,bool isProc)
        {
            try
            {
                sqlConnect = getConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConnect;
                if (sqlConnect != null) //数据库连接成功
                {
                    if (sqlConnect.State != ConnectionState.Open)
                        sqlConnect.Open();
                    if (sqlConnect.State == ConnectionState.Open)
                    {
                        SqlTransaction trans; //定义事务变量
                        trans = sqlConnect.BeginTransaction(); //开始事务                   
                        int i;
                        //循环执行数组命令
                        for (i = 0; i < strcmd.Length; i++)
                        {

                            sqlCmd.Connection = sqlConnect;
                            sqlCmd.CommandTimeout = 0;
                            if (isProc)
                            {
                                /*
                               //存储过程设置，Version2.0.0.0
                               //*/
                                sqlCmd.CommandType = CommandType.StoredProcedure;
                                sqlCmd.CommandText = "upCommand";
                                sqlCmd.Parameters.Clear();
                                sqlCmd.Parameters.AddWithValue("@cmd", strcmd[i]);

                            }
                            else
                            {
                                /*
                                Version1.0.0.0
                                **/
                                sqlCmd.CommandType = CommandType.Text;//字符串命令类型
                                sqlCmd.CommandText = strcmd[i];                         
                        
                            }
                             sqlCmd.Transaction = trans; //命令获取事务
                            if (sqlCmd.ExecuteNonQuery() <0)
                            {
                                //执行失败，事务回滚。
                                trans.Rollback();
                                return 0;
                            }
                        }
                        trans.Commit(); //执行事务;事务结束点。能执行则成功
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                sqlConnect.Close();
            }
        }
        /// <summary>
        /// 数据库命令操作
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public int sqlCommand(SqlCommand[] cmd)
        {

            try
            {               
                sqlConnect = getConnection();
                if (sqlConnect != null) //数据库连接成功
                {
                    if (sqlConnect.State != ConnectionState.Open)
                        sqlConnect.Open();
                    if (sqlConnect.State == ConnectionState.Open)
                    {
                        SqlTransaction trans; //定义事务变量
                        trans = sqlConnect.BeginTransaction(); //开始事务
                        int i;
                        //循环执行数组命令
                        for (i = 0; i <cmd.Length; i++)
                        {

                            cmd[i].Connection = sqlConnect;
                            cmd[i].CommandTimeout = 0;
                            cmd[i].Transaction = trans; //命令获取事务
                            if (cmd[i].ExecuteNonQuery() <0)
                            {
                                //执行失败，事务回滚。
                                trans.Rollback();
                                return 0;
                            }
                        }
                        trans.Commit(); //执行事务;事务结束点。能执行则成功
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                sqlConnect.Close();
            }
        }        
        /// <summary>
        /// 利用数据库操作对象进行数据库操作
        /// </summary>
        /// <param name="cmd">数据库操作命令对象</param>
        /// <returns>成功返回大于0的整数，否则返回0或小于0的整数</returns>
        public  int sqlCommand(SqlCommand cmd)
        {
            try
            {
                sqlConnect = getConnection();
                if (sqlConnect!=null)
                {
                    cmd.Connection = sqlConnect;
                    if (sqlConnect.State!=ConnectionState.Open)
                    sqlConnect.Open();
                    if (sqlConnect.State == ConnectionState.Open)
                    {
                     return cmd.ExecuteNonQuery();
                    }
                    else
                        return -1;  
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                sqlConnect.Close();
            }
        }
        
        /// <summary>
        /// 利用存储过程进行数据库操作。
        /// </summary>
        /// <param name="cmd">数据库操作命令对象</param>
        /// <returns>返回值大于0表示操作成功，否则表示失败</returns>
        public int sqlCommandSp(SqlCommand cmd) 
        {
            try
            {
                SqlConnection sqlConnect = null;
                sqlConnect = getConnection();
                if (sqlConnect!=null) //数据库连接成功
                {
                    cmd.Connection = sqlConnect; //tmpcmd获取连接对象
                    if (sqlConnect.State!=ConnectionState.Open)
                     sqlConnect.Open();
                    if (sqlConnect.State == ConnectionState.Open)
                    {
                        cmd.CommandType = CommandType.StoredProcedure; //设置命令类型为存储过程                    
                        return cmd.ExecuteNonQuery();                        
                    }
                    else
                    {
                        return -1;
                    }                    
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                sqlConnect.Close();
            }
        }
        /// <summary>
        /// 利用存储过程进行数据库操作。
        /// </summary>
        /// <param name="cmd">数据库操作命令对象</param>
        /// <param name="blReturn">是否带有获取return返回值</param>
        /// <returns>返回值大于0表示操作成功，否则表示失败</returns>
        public int sqlCommandSp(SqlCommand cmd,bool blReturn)
        {
            try
            {
                SqlConnection sqlConnect = null;
                sqlConnect = getConnection();
                if (sqlConnect != null) //数据库连接成功
                {
                    cmd.Connection = sqlConnect; //tmpcmd获取连接对象
                    if (sqlConnect.State != ConnectionState.Open)
                        sqlConnect.Open();
                    if (sqlConnect.State == ConnectionState.Open)
                    {
                        cmd.CommandType = CommandType.StoredProcedure; //设置命令类型为存储过程                    
                        if (blReturn)
                        {
                            cmd.Parameters.Add(new SqlParameter("@return", SqlDbType.Int));
                            cmd.Parameters["@return"].Direction = ParameterDirection.ReturnValue;
                            cmd.ExecuteNonQuery();
                            return Convert.ToInt32(cmd.Parameters["@return"].Value);
                        }
                        else
                           return cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                    return 0;
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                sqlConnect.Close();
            }
        }
        
        /// <summary>
        /// 利用存储过程进行数据库操作。
        /// </summary>
        /// <param name="cmd">数据库操作命令对象，返回的参数为@return</param>         
        /// <returns>返回值为字符串型，由数据库返回信息决定,字符串信息长度不能大于500</returns>
        public string sqlCommandByReturn(SqlCommand cmd)
        {
            try
            {
                SqlConnection sqlConnect = null;
                sqlConnect = getConnection();
                if (sqlConnect != null) //数据库连接成功
                {
                    cmd.Connection = sqlConnect; //tmpcmd获取连接对象
                    if (sqlConnect.State != ConnectionState.Open)
                        sqlConnect.Open();
                    if (sqlConnect.State == ConnectionState.Open)
                    {
                        cmd.CommandType = CommandType.StoredProcedure; //设置命令类型为存储过程                    

                        cmd.Parameters.Add(new SqlParameter("@return", SqlDbType.NVarChar,500));
                        cmd.Parameters["@return"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        return cmd.Parameters["@return"].Value.ToString();
                    }
                    else
                    {
                        return "数据库连接失败！";
                    }
                }
                else
                    return "数据库连接对象为空！";
            }
            catch (Exception)
            {
                return "数据库操作发生错误！";
            }
            finally
            {
                sqlConnect.Close();
            }
        }
        /// <summary>
        /// 利用存储过程完成数据库操作
        /// </summary>
        /// <param name="tmpcmd">命令对象数组</param>
        /// <returns>返回值大于0表示操作成功，否则表示失败</returns>
        public int sqlCommandSp(SqlCommand[] tmpcmd)
        {
            try
            {                
                sqlConnect = getConnection();
                if (sqlConnect!=null) //连接成功
                { 
                    if (sqlConnect.State!=ConnectionState.Open)
                     sqlConnect.Open();
                    if (sqlConnect.State == ConnectionState.Open)
                    { 
                    SqlTransaction trans; //定义事务变量                   
                    trans = sqlConnect.BeginTransaction();//事务对象获取值
                    int i = 0;
                    //循环执行存储过程命令，如果失败将会执行会滚。
                    for (i = 0; i < tmpcmd.Length; i++)
                    {
                        tmpcmd[i].Connection = sqlConnect;
                        tmpcmd[i].CommandTimeout = 0;
                        tmpcmd[i].CommandType = CommandType.StoredProcedure;
                        if (tmpcmd[i].ExecuteNonQuery() <0)
                        {
                            trans.Rollback();
                            return 0;
                        }
                    }
                    trans.Commit();
                    return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                    return 0;
            }
            catch (Exception)
            { return -1; }
            finally
            {
                sqlConnect.Close();
            }
        }
          /// <summary>
        /// 查询
        /// </summary>
        /// <param name="cmd">查询命令</param>
        /// <returns></returns>
        public DataSet dbToDS(SqlCommand cmd,string strTableName)
        {
            try
            {               
                DataSet ds = new DataSet(); //创建ds数据库对象
                sqlConnect = getConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConnect;
                SqlDataAdapter sqlDA = new SqlDataAdapter();              
                if (sqlConnect != null) //数据库连接成功
                {
                    if (sqlConnect.State != ConnectionState.Open)
                        sqlConnect.Open();
                    if (sqlConnect.State == ConnectionState.Open)
                    {
                        sqlCmd = cmd;
                        sqlCmd.Connection = sqlConnect;
                        sqlDA.SelectCommand = sqlCmd;                       
                        ds.Clear();                        
                        sqlDA.Fill(ds, strTableName); //填充数据集
                        return ds; //返回数据集
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                sqlConnect.Close();
            }
        }
        /// <summary>
        /// 多命令查询操作
        /// </summary>
        /// <param name="cmd">查询命令数组</param>
        /// <param name="strTableName">数据表格名称</param>
        /// <returns>结果返回在数据集中</returns>
        public  DataSet dbToDS(SqlCommand[] cmd, string[] strTableName)
        {
            try
            {
                DataSet ds = new DataSet(); //创建ds数据库对象                        
                sqlConnect = getConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConnect;
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                if (sqlConnect != null) //数据库连接成功
                {
                    if (sqlConnect.State != ConnectionState.Open)
                        sqlConnect.Open();
                    if (sqlConnect.State == ConnectionState.Open)
                    {
                         ds.Clear();                         
                         for (int i = 0; i < cmd.Length; i++)
                         {
                         sqlCmd = cmd[i];
                         sqlCmd.Connection = sqlConnect;
                         sqlDA.SelectCommand = sqlCmd;                        
                        sqlDA.Fill(ds, strTableName[i]); //填充数据集
                         }
                         
                        return ds; //返回数据集
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                sqlConnect.Close();
            }
        }
        /// <summary>
        /// 多命令查询操作
        /// </summary>
        /// <param name="cmd">查询命令</param>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        public DataSet dbToDS(string[] cmd, string[] strTableName)
        {
            try
            {
                DataSet ds = new DataSet(); //创建ds数据库对象                        
                sqlConnect = getConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConnect;
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                if (sqlConnect != null) //数据库连接成功
                {
                    if (sqlConnect.State != ConnectionState.Open)
                        sqlConnect.Open();
                    if (sqlConnect.State == ConnectionState.Open)
                    {
                        ds.Clear();                       
                        sqlCmd.Connection = sqlConnect;
                        for (int i = 0; i < cmd.Length; i++)
                        {
                            sqlCmd.CommandText = cmd[i];
                            sqlDA.SelectCommand = sqlCmd;
                            sqlDA.Fill(ds, strTableName[i]); //填充数据集
                        }                        
                        return ds; //返回数据集
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                sqlConnect.Close();
            }
        }
        /// <summary>
        /// 根据指定参数命令strcmd,从数据库中查询所需数据，以数据集返回。strTableName返回数据集中的表名。
        /// 支持存储过程，需要先建立以下存储过程格式：
        /// alter proc upCommand
        ///@cmd nvarchar(2000)
        ///as 
        ///exec (@cmd)
        /// </summary>
        /// <param name="strcmd">参数命令</param>
        /// <param name="strTableName">返回数据集中的表名</param>
        /// <returns>从数据库中查询所需数据，以数据集返回</returns>
        public DataSet dbToDS(string strcmd, string strTableName) 
        {
            try
            {
                DataSet ds = new DataSet(); //创建ds数据库对象                
                sqlConnect = getConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConnect;
                SqlDataAdapter sqlDA = new SqlDataAdapter();                
                if (sqlConnect!=null) //数据库连接成功
                {
                    if (sqlConnect.State!=ConnectionState.Open)
                      sqlConnect.Open();
                    if (sqlConnect.State == ConnectionState.Open)
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "upCommand";                       
                        sqlCmd.Parameters.AddWithValue("@cmd", strcmd);
                        sqlDA.SelectCommand = sqlCmd;                   
                        ds.Clear();                   
                        sqlDA.Fill(ds, strTableName); //填充数据集
                        return ds; //返回数据集
                    }
                    else
                    {
                        return null;
                    }
                   
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                sqlConnect.Close();
            }
        }
        /// <summary>
        /// 查询返回数据表格
        /// </summary>
        /// <param name="strCmd">查询命令</param>
        /// <returns></returns>
        public DataTable dbToDT(string strCmd)
        {
            DataSet ds = dbToDS(strCmd, "temp");
            if (ds == null)
                return null;
            else
                return ds.Tables["temp"];

        }
        /// <summary>
        /// 根据指定参数命令strcmd,从数据库中查询所需数据，以数据集返回。strTableName返回数据集中的表名。
        /// 支持存储过程，需要先建立以下存储过程格式：
        /// alter proc upCommand
        ///@cmd nvarchar(2000)
        ///as 
        ///exec (@cmd)
        /// </summary>
        /// <param name="strcmd">参数命令</param>
        /// <param name="strTableName">返回数据集中的表名</param>
        /// <param name="isProc">是否采用upCommand存储过程支持，true表示采用，false表示不用</param>
        /// <returns>从数据库中查询所需数据，以数据集返回</returns>
        public DataSet dbToDS(string strcmd, string strTableName,bool isProc)
        {
            try
            {
                DataSet ds = new DataSet(); //创建ds数据库对象                
                sqlConnect = getConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConnect;
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                if (sqlConnect != null) //数据库连接成功
                {
                    if (sqlConnect.State != ConnectionState.Open)
                        sqlConnect.Open();
                    if (sqlConnect.State == ConnectionState.Open)
                    {
                        if (isProc)
                        {
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            sqlCmd.CommandText = "upCommand";
                            sqlCmd.Parameters.AddWithValue("@cmd", strcmd);
                        }
                        else
                        {
                            sqlCmd.CommandType = CommandType.Text;
                            sqlCmd.CommandText = strcmd;                            
                        }                        
                        sqlDA.SelectCommand = sqlCmd;
                        ds.Clear();
                        sqlDA.Fill(ds, strTableName); //填充数据集
                        return ds; //返回数据集
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                sqlConnect.Close();
            }
        }
        /// <summary>
        /// 根据指定参数命令cmd,从数据库中查询所需数据，以数据集返回。strTableName返回数据集中的表名。
        /// </summary>
        /// <param name="cmd">查询命令对象</param>
        /// <param name="strTableName">返回数据集中的表名</param>
        /// <returns>以数据集形式返回数据，否则返回null</returns>
        public DataSet dbToDSSp(SqlCommand cmd, string strTableName) 
        {
            try
            {               
                sqlConnect = getConnection();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                if (sqlConnect!=null) //连接成功
                {
                    if (sqlConnect.State!=ConnectionState.Open)
                      sqlConnect.Open();
                     if (sqlConnect.State == ConnectionState.Open)
                     {
                     cmd.Connection = sqlConnect;
                    cmd.CommandType = CommandType.StoredProcedure; //设置命令类型为存储过程
                    sqlDA.SelectCommand = cmd;
                    DataSet ds = new DataSet(); //创建数据集对象                   
                    ds.Clear();                   
                    sqlDA.Fill(ds, strTableName); //填充数据集
                    return ds;
                     }
                     else
                     {
                         return null;
                     }
                    
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                sqlConnect.Close();
            }
        }
         /// <summary>
        /// 恢复数据库中的文件
        /// </summary>
        /// <param name="strcmd">查询命令</param>
        /// <param name="fileName">恢复的文件名</param>
        /// <param name="fileType">文件类型</param>
        /// <param name="filePath">存放路径</param>
        /// <param name="fileField">数据库中存放文件的字段名</param>
        /// <returns>成功返回大于0的整数，否则表示失败</returns>
        public int restoreFile(string strcmd, string fileName, string fileType, string filePath, string fileField) 
        {
            try
            {
                DataSet ds = new DataSet("temp");
                if (fileType.Length <= 0) return 0; //文件类型不合法，返回0
                ds = dbToDS(strcmd, "temp");//根据查询指令获取数据
                if (ds != null)
                {
                    byte[] b = (byte[])ds.Tables["temp"].Rows[0][fileField];
                    if (b.Length > 0)
                    {
                        System.IO.FileStream stream1 = new System.IO.FileStream(filePath + fileName + "." + fileType, System.IO.FileMode.OpenOrCreate);
                        stream1.Write(b, 0, b.Length);
                        return 1;
                    }
                    else
                        return 0;
                }
                else
                    return 0;

            }
            catch (Exception)
            {
                return 0;
            }            
        }
        /// <summary>
        /// 备份数据库，path备份完整的数据库路经
        /// </summary>
        /// <param name="sourceDBName">源数据库名</param>
        /// <param name="path">别分路径</param>
        /// <returns>返回1表示成功，否则表示失败</returns>
        public int backupDB(string sourceDBName, string path)
        {
            try
            {
                sqlCommand("BACKUP DATABASE " + sourceDBName + " TO disk='" + path + ".bak'");
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 还原数据库
        /// </summary>
        /// <param name="sourceDBName">源数据库名</param>
        /// <param name="path">还原路径</param>
        /// <returns>返回1表示成功，否则表示失败</returns>
        public int restoreDB(string sourceDBName, string path)
        {
            try
            {
                sqlCommand("RESTORE DATABASE " + sourceDBName + " FROM DISK = '" + path + "'");
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 获取指定服务器上所有的数据库，以数据表格形式放回；
        /// 如果存在返回在DataTable中，否则返回null;
        /// </summary>
        /// <returns></returns>
        public DataTable getAllDB()
        {
            DataSet ds = dbToDS("SELECT * FROM sys.databases", "temp", false);//获取服务器上数据库
            if (ds != null)
            {
                if (ds.Tables["temp"] != null)
                    return ds.Tables["temp"];
                else
                    return null;
            }
            else
                return null;
        }
        /// <summary>
        /// 返回当前服务器上指定数据库上所有的用户表结构，
        /// 以DataTable形式返回，否则返回null
        /// </summary>
        /// <returns></returns>
        public DataTable getAllUserTable()
        {
            DataSet ds = dbToDS("SELECT * FROM sysobjects WHERE xtype = 'U'", "temp", false);//获取服务器上数据库
            if (ds != null)
            {
                if (ds.Tables["temp"] != null)
                    return ds.Tables["temp"];
                else
                    return null;
            }
            else
                return null;
        
        }
#endregion
    }    
   
    /// <summary>
    /// FileName:OLEDB数据库访问类
    /// Description:提供OLEDB类型数据库操作的标准类。
    /// </summary>
    public class DBOLE 
    {
        #region var
            private string strConnect = "";
            private string strService = "";  // 定义服务器名称
            private string strDataBase = ""; //定义数据库名称
            private string strUser = "";  //定义登陆用户名
            private string strPassword = ""; //定义登陆用户密码
            private int intType = 0;  //定义连接类型  
        #endregion
        #region property
        /// <summary>
        /// 连接服务器名称
        /// </summary>
        public string serviceName
        {
            get { return strService; }
            set { strService = value; }
        }
        /// <summary>
        /// 连接数据库名称
        /// </summary>
        public string databaseName
        {
            get { return strDataBase; }
            set { strDataBase = value; }
        }
        /// <summary>
        /// 连接数据库用户名称
        /// </summary>
        public string userName
        {
            get { return strUser; }
            set { strUser = value; }
        }
        /// <summary>
        /// 连接数据库密码
        /// </summary>
        public string password
        {
            get { return strPassword; }
            set { strPassword = value; }
        }
        /// <summary>
        /// 连接数据库类型
        /// </summary>
        public int type
        {
            get { return intType; }
            set { intType = value; }
        }            
        #endregion
        #region method
        /// <summary>
        /// 构造函数
        /// </summary>
        public DBOLE(string dbPath)
        {
           strDataBase =dbPath ;
            // oleConnect = getOleConnection();
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public DBOLE() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strD">数据库名</param>
        /// <param name="strU">用户名</param>
        /// <param name="strP">密码</param>
        /// <param name="intT">连接类型</param>
        public DBOLE(string strD, string strU, string strP, int intT)
        {
            databaseName = strD;
            userName = strU;
            password = strP;
            type = intT;
        }
        /// <summary>
        /// 测试连接的抽象方法，返回值如果为true表示测试连接成功，否则表示失败
        /// </summary>
        /// <returns></returns>
        public bool testConnect()
        {
            
            switch (type)
            {
                case 0:
                    strConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databaseName + ";User Id=" + userName + ";Password=" + password + "; ";
                    break;
                case 1:
                    strConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databaseName + ";Jet OLEDB:Database Password=" + password + ";";
                    break;
                case 2:
                    strConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databaseName + ";Jet OLEDB:System Database=system.mdw;";
                    break;
                case 3:
                    strConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databaseName + ";Jet OLEDB:System Database=system.mdw;User ID=" + userName + ";Password=" + password + ";";
                    break;
                default:
                    strConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databaseName + ";Jet OLEDB:System Database=system.mdw;";
                    break;
            }
            OleDbConnection oleConnect = new OleDbConnection();
            try
            {
                
                oleConnect.ConnectionString = strConnect;                
                oleConnect.Open();
                if (oleConnect.State == ConnectionState.Open)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                oleConnect.Close();
            }
        }
        /// <summary>
        /// 获取数据库接对象
        /// </summary>
        /// <returns></returns>
        private OleDbConnection getOleConnection()
        {
            try
            {
                switch (type)
                {
                    case 0:
                        strConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databaseName + ";User Id=" + userName + ";Password=" + password + "; ";
                        break;
                    case 1:
                        strConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databaseName + ";Jet OLEDB:Database Password=" + password + ";";
                        break;
                    case 2:
                        strConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databaseName + ";Jet OLEDB:System Database=system.mdw;";
                        break;
                    case 3:
                        strConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databaseName + ";Jet OLEDB:System Database=system.mdw;User ID=" + userName + ";Password=" + password + ";";
                        break;
                    default:
                        strConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databaseName + ";Jet OLEDB:System Database=system.mdw;";
                        break;
                }
                OleDbConnection oleConnect=new OleDbConnection();
                oleConnect.ConnectionString = strConnect;
                return oleConnect;
               
            }
            catch (Exception)
            {
                return null;
            }            
        }
        /// <summary>
        /// 数据库操作命令。
        /// </summary>
        /// <param name="cmd">数据库命令字符串</param>
        /// <returns>返回值大于0表示操作成功，否则表示失败</returns>
        public int sqlCommand(string cmdText)
        {
            
            OleDbConnection connect = getOleConnection();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connect;
            try
            {
                cmd.CommandText = cmdText;
                connect.Open();
                return cmd.ExecuteNonQuery();
               
                
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                connect.Close();
            }
        }
        /// <summary>
        /// 多个数据库操作命令。
        /// </summary>
        /// <param name="strcmd">数据库操作命令数组</param>
        /// <returns>返回值大于0表示操作成功，否则表示失败</returns>
        public int sqlCommand(string[] strcmd)
        {
            
            OleDbConnection connect = getOleConnection();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connect;
              try
              {  
                if (connect != null) //数据库连接成功
                {
                    if (connect.State != ConnectionState.Open)
                        connect.Open();
                    if (connect.State == ConnectionState.Open)
                    {
                        OleDbTransaction trans; //定义事务变量
                        trans = connect.BeginTransaction(); //开始事务                   
                        int i;
                        //循环执行数组命令
                        for (i = 0; i < strcmd.Length; i++)
                        {

                            cmd.Connection = connect;
                            cmd.CommandTimeout = 0;

                            cmd.CommandType = CommandType.Text;//字符串命令类型
                            cmd.CommandText = strcmd[i];
                            cmd.Transaction = trans; //命令获取事务
                            if (cmd.ExecuteNonQuery() <= 0)
                            {
                                //执行失败，事务回滚。
                                trans.Rollback();
                                return 0;
                            }
                        }
                        trans.Commit(); //执行事务;事务结束点。能执行则成功
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                connect.Close();
            }
        }
        /// <summary>
        /// 数据库命令对象对数据库进行操作
        /// </summary>
        /// <param name="cmd">数据库命令对象</param>
        /// <returns>返回值大于0表示操作成功，否则表示失败</returns>
        public int sqlCommand(OleDbCommand cmd)
        {
            OleDbConnection connect = getOleConnection();
            if (cmd == null) return -1;
            try
            {
                cmd.Connection = connect;
                connect.Open();
                return cmd.ExecuteNonQuery();
               
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                connect.Close();
            }
        }
        /// <summary>
        /// 数据库命令操作
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public int sqlCommand(OleDbCommand[] cmd)
        {
            OleDbConnection connect = getOleConnection();            
            try
            {
                if (connect != null) //数据库连接成功
                {
                    if (connect.State != ConnectionState.Open)
                        connect.Open();
                    if (connect.State == ConnectionState.Open)
                    {
                        OleDbTransaction trans; //定义事务变量
                        trans = connect.BeginTransaction(); //开始事务
                        int i;
                        //循环执行数组命令
                        for (i = 0; i < cmd.Length; i++)
                        {

                            cmd[i].Connection = connect;
                            cmd[i].CommandTimeout = 0;
                            cmd[i].Transaction = trans; //命令获取事务
                            if (cmd[i].ExecuteNonQuery() <= 0)
                            {
                                //执行失败，事务回滚。
                                trans.Rollback();
                                return 0;
                            }
                        }
                        trans.Commit(); //执行事务;事务结束点。能执行则成功
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                connect.Close();
            }
        }        
         /// <summary>
        /// 根据指定参数命令strcmd,从数据库中查询所需数据，以数据集返回。strTableName返回数据集中的表名。
        /// </summary>
        /// <param name="strcmd">查询命令字符串</param>
        /// <param name="strTableName">数据集中的表明</param>
        /// <returns>以数据集返回</returns>
        public DataSet dbToDS(string strcmd, string strTableName)
        {
            OleDbConnection connect = getOleConnection();
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                cmd.Connection = connect;
                cmd.CommandText = strcmd;
                OleDbDataAdapter oleDA = new OleDbDataAdapter();//创建数据适配器
                oleDA.SelectCommand = cmd;
                connect.Open();
                DataSet ds = new DataSet();
                oleDA.Fill(ds, strTableName);
                return ds;               
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connect.Close();
            }
        }       
        /// <summary>
        /// 恢复数据库中的文件
        /// </summary>
        /// <param name="strcmd">查询命令</param>
        /// <param name="fileName">恢复的文件名</param>
        /// <param name="fileType">文件类型</param>
        /// <param name="filePath">存放路径</param>
        /// <param name="fileField">数据库中存放文件的字段名</param>
        /// <returns>成功返回大于0的整数，否则表示失败</returns>
        public int restoreFile(string strcmd, string fileName, string fileType, string filePath, string fileField)
        {
            try
            {
                DataSet ds = new DataSet("temp");
                if (fileType.Length <= 0) return 0; //文件类型不合法，返回0
                ds = dbToDS(strcmd, "temp");//根据查询指令获取数据
                if (ds != null)
                {
                    byte[] b = (byte[])ds.Tables["temp"].Rows[0][fileField];
                    if (b.Length > 0)
                    {
                        System.IO.FileStream stream1 = new System.IO.FileStream(filePath + fileName + "." + fileType, System.IO.FileMode.OpenOrCreate);
                        stream1.Write(b, 0, b.Length);
                        return 1;
                    }
                    else
                        return 0;
                }
                else
                    return 0;

            }
            catch (Exception)
            {
                return 0;
            }            
        }
        /// <summary>
        /// 备份数据库，path备份完整的数据库路经
        /// </summary>
        /// <param name="sourceDBName">源数据库名</param>
        /// <param name="path">别分路径</param>
        /// <returns>返回1表示成功，否则表示失败</returns>
        public int backupDB(string sourceDBName, string path)
        {
            try
            {
                sqlCommand("BACKUP DATABASE " + sourceDBName + " TO disk='" + path + ".bak'");
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        /// <summary>
        /// 还原数据库
        /// </summary>
        /// <param name="sourceDBName">源数据库名</param>
        /// <param name="path">还原路径</param>
        /// <returns>返回1表示成功，否则表示失败</returns>
        public int restoreDB(string sourceDBName, string path)
        {
            try
            {
                sqlCommand("RESTORE DATABASE " + sourceDBName + " FROM DISK = '" + path + "'");
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        #endregion
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using IBatisNet.Common.Exceptions;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Scope;
using Utility;
namespace DBCtrl.DAO
{
    public class BaseSqlMapDao
    {
        protected static volatile ISqlMapper SqlMap;
        protected static readonly log4net.ILog Logger = LogHelper.AppLoger;
        protected BaseSqlMapDao()
        {
            if (SqlMap != null) return;
            lock (typeof(SqlMapper))
            {
                if (SqlMap == null)
                    InitMapper();
            }
        }

        private static void InitMapper()
        {
            var builder = new DomSqlMapBuilder();
            var file = AppDomain.CurrentDomain.BaseDirectory + @"SqlMap.config";
            SqlMap = builder.Configure(file);
            //ConnectInfo.Instance.UpdateDBInfo(SqlMap.DataSource.ConnectionString);
        }

        protected IList ExecuteQueryForList(string statementName, object parameterObject)
        {
            try
            {
                return SqlMap.QueryForList(statementName, parameterObject);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return null;
            }
        }
        protected IList ExecuteQueryForList(string statementName, object parameterObject, int skipResults, int maxResults)
        {
            try
            {
                return SqlMap.QueryForList(statementName, parameterObject, skipResults, maxResults);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return null;
            }
        }

        protected IList<T> ExecuteQueryForList<T>(string statementName, object parameterObject)
        {
            try
            {
                return SqlMap.QueryForList<T>(statementName, parameterObject);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return null;
            }
        }

        protected IList<T> ExecuteQueryForList<T>(string statementName, object parameterObject, int skipResults, int maxResults)
        {
            try
            {
                return SqlMap.QueryForList<T>(statementName, parameterObject, skipResults,
                                              maxResults);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return null;
            }
        }

        protected object ExecuteQueryForObject(string statementName, object parameterObject)
        {
            try
            {
                return SqlMap.QueryForObject(statementName, parameterObject);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return null;
            }
        }
        protected T ExecuteQueryForObject<T>(string statementName, object parameterObject)
        {
            try
            {
                return SqlMap.QueryForObject<T>(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new IBatisNetException("Error executing query '" + statementName + "' for object<T>.  Cause: " + e.Message, e);
            }
        }
        protected int ExecuteUpdate(string statementName, object parameterObject)
        {
            try
            {
                return SqlMap.Update(statementName, parameterObject);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return 0;
            }
        }

        protected bool ExecuteInsert(string statementName, object parameterObject)
        {
            try
            {
                SqlMap.Insert(statementName, parameterObject);
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return false;
            }
        }

        protected int ExecuteDelete(string statementName, object parameterObject)
        {
            try
            {
                return SqlMap.Delete(statementName, parameterObject);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                return 0;
            }
        }

        #region GetSql/GetDataTable
        public string GetSql(string statementName, object paramObject)
        {
            IMappedStatement statement = SqlMap.GetMappedStatement(statementName);
            if (!SqlMap.IsSessionStarted)
            {
                SqlMap.OpenConnection();
            }
            RequestScope scope = statement.Statement.Sql.GetRequestScope(statement,
                                                                         paramObject,
                                                                         SqlMap.
                                                                             LocalSession);
            return scope.PreparedStatement.PreparedSql;
        }
        public DataTable ExecuteDataTable(string statementName, object paramObject)
        {
            try
            {
                var ds = new DataSet();
                IMappedStatement statement = SqlMap.GetMappedStatement(statementName);
                if (!SqlMap.IsSessionStarted)
                {
                    SqlMap.OpenConnection();
                }
                RequestScope scope = statement.Statement.Sql.GetRequestScope(statement,
                                                                             paramObject,
                                                                             SqlMap.
                                                                                 LocalSession);

                statement.PreparedCommand.Create(scope, SqlMap.LocalSession,
                                                 statement.Statement, paramObject);

                IDbCommand dc =
                    SqlMap.LocalSession.CreateCommand(scope.IDbCommand.CommandType);

                dc.CommandText = scope.IDbCommand.CommandText;

                if (scope.IDbCommand.Parameters != null)
                {
                    foreach (IDbDataParameter para in scope.IDbCommand.Parameters)
                    {
                        IDbDataParameter param = SqlMap.LocalSession.CreateDataParameter();
                        param.ParameterName = para.ParameterName;
                        param.Value = para.Value;
                        dc.Parameters.Add(param);
                    }
                }
                IDbDataAdapter dda = SqlMap.LocalSession.CreateDataAdapter(dc);
                dda.Fill(ds);

                return ds.Tables[0];
            }
            catch (Exception e)
            {
                throw new IBatisNetException(
                    "Error executing query '" + statementName + "' for list.  Cause: " +
                    e.Message, e);
            }
        }

        #endregion GetSql/GetDataTable
    }
}
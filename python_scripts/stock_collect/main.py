import tushare as ts
import pandas as pd
from mssql_utils import MsSqlOperater
from tushare_utils import init_fundamental

_SQLSERVER_USER = "sa"
_SQLSERVER_PWD = "wgq!123456"
_SQLSERVER_HOST = "127.0.0.1"
_SQLSERVER_DB = "tusharedb"

sqlOperater = MsSqlOperater(_SQLSERVER_USER, _SQLSERVER_PWD, _SQLSERVER_HOST, _SQLSERVER_DB)

engine = sqlOperater.get_engine()

init_fundamental(engine)


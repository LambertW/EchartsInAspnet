import tushare as ts
import datetime
from mssql_utils import mapping_df_types

_TEMP_PREFIX = "temp_"
_now = datetime.datetime.now()
_year = _now.year
_month = _now.month

def to_sql(engine, df, table_name):
    df.infer_objects()
    dtypedict = mapping_df_types(df)
    df.to_sql(_TEMP_PREFIX + table_name, con=engine,
              if_exists='replace', index=False, dtype=dtypedict)
    return _TEMP_PREFIX + table_name

def get_lastQuarter():
    """上一季度"""
    return _month / 3

def init_fundamental(engine):
    """基本面数据"""
    # 股票列表
    df = ts.get_stock_basics()
    # df.to_csv('data/股票列表.csv')
    to_sql(engine, df, 'stock_basics')

    # 业绩报告（主表）(2月份之前)
    # 当前只获取上一季度报告
    df = ts.get_report_data(_year, 2)
    to_sql(engine, df, 'report_data')

    # 盈利能力(2月份之前)
    df = ts.get_profit_data(_year, 2)
    to_sql(engine, df, 'profit_data')

    # 每日收盘行情
    # df = ts.get_day_all('20180921')
    #df.to_csv('data/每日收盘行情.csv', index=False)
    # df.to_sql('temp_stock_daily', engine)
    # print(df.dtypes)

from sqlalchemy import create_engine, Column, String
from sqlalchemy import NVARCHAR, Float, Integer, DECIMAL

class MsSqlOperater():
    def __init__(self, user, pwd, host, db):
        self.user = user
        self.pwd = pwd
        self.host = host
        self.db = db

    def get_engine(self):
        "定义引擎"
        engine = create_engine(
            "mssql+pymssql://" + self.user + ":" + self.pwd +
            "@" + self.host + "/" + self.db + "?charset=utf8",
        )
        return engine

def mapping_df_types(df):
    """
    类型转换
    """
    dtypedict = {}
    for i,j in zip(df.columns, df.dtypes):
        if "object" in str(j):
            dtypedict.update({i: NVARCHAR(length=255)})
        if "float" in str(j):
            dtypedict.update({i: DECIMAL(precision=12, scale=2)})
        if "int" in str(j):
            dtypedict.update({i: Integer()})
    return dtypedict

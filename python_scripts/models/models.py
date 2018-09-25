# coding: utf-8
from sqlalchemy import Column, DateTime, Integer, Unicode
from sqlalchemy.ext.declarative import declarative_base

Base = declarative_base()
metadata = Base.metadata


class TblStockBasic(Base):
    __tablename__ = 'tbl_stock_basic'

    id = Column(Integer, primary_key=True)
    ts_code = Column(Unicode(20))
    symbol = Column(Unicode(10))
    name = Column(Unicode(255))
    fullname = Column(Unicode(255))
    enname = Column(Unicode(255))
    exchange_id = Column(Unicode(50))
    curr_type = Column(Unicode(10))
    list_status = Column(Unicode(10))
    list_date = Column(DateTime)
    delist_date = Column(DateTime)
    is_hs = Column(Unicode(10))

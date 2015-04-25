from sqlalchemy import ForeignKeyConstraint
from sqlalchemy.orm import relationship, backref

from sqlalchemy.ext.associationproxy import association_proxy

from application import db

class StationBase(object):
    def refuel(liters, kilometers):
        self.liters += liters
        self.kilometers += kilometers
        self.average_consumption = (self.liters / self.kilometers) * 100

class UserGasStation(db.Model, StationBase):
    __tablename__ = 'user_gas_station'
    user_id = db.Column(db.String(255), db.ForeignKey("users.id"), primary_key=True)
    gas_station_city = db.Column(db.String(80), primary_key=True)
    gas_station_address = db.Column(db.String(80), primary_key=True)
    kilometers = db.Column(db.Float)
    liters = db.Column(db.Float)
    user = db.relationship('User')
    gas_station = db.relationship('GasStation')
    __table_args__ = (ForeignKeyConstraint([gas_station_city, gas_station_address], ["gas_stations.city", "gas_stations.address"]), {})

    def __init__(self, user_id, gas_station_city, gas_station_address):
        self.user_id = user_id
        self.gas_station_city = gas_station_city
        self.gas_station_address = gas_station_address

    def __repr__(self):
        return '<Id {}>'.format(self.id)

class GasStation(db.Model, StationBase):
    __tablename__ = "gas_stations"
    city = db.Column(db.String(80), primary_key=True)
    address = db.Column(db.String(80), primary_key=True)
    name = db.Column(db.String(80))
    kilometers = db.Column(db.Float)
    liters = db.Column(db.Float)
    average_consumption = db.Column(db.Float)

    def __init__(self, city, address, name):
        self.city = city
        self.address = address
        self.name = name
        self.kilometers = 0
        self.liters = 0
        self.average_consumption = 100

    def __repr__(self):
        return '<Name: {0}; City: {1}; Address: {2}>'.format(self.name, self.city, self.address)

class User(db.Model):
    __tablename__ = 'users'
    id = db.Column(db.String(255), primary_key=True)
    _gas_stations = db.relationship("GasStation")
    gas_stations = association_proxy('_gas-stations', "gas_station",
        creator=lambda _i: UserGasStation(image=_i))

    def __init__(self, id):
        self.id = id

    def __repr__(self):
        return '<Id {}>'.format(self.id)

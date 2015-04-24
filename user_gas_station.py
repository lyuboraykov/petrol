from app import db
from gas_station import GasStation

class UserGasStation(db.Model):
    user_id = db.Column(db.String(255), ForeignKey("users.id"), primary_key=True)
    gas_station_city = db.Column(db.String(80), primary_key=True)
    gas_station_address = db.Column(db.String(80), primary_key=True)
    __table_args__ = (ForeignKeyConstraints([gas_station_city, gas_station_address],
                                            [GasStation.city, GasStation.address]), {})

    kilometers = db.Column(db.Float)
    liters = db.Column(db.Float)

    def __init__(self, user_id, gas_station_city, gas_station_address):
        self.user_id = user_id
        self.gas_station_city = gas_station_city
        self.gas_station_address = gas_station_address

    def __repr__(self):
        return '<Id {}>'.format(self.id)

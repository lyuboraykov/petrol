from app import db

class GasStation(db.Model):
    city = db.Column(db.String(80), primary_key=True)
    address = db.Column(db.String(80), primary_key=True)
    name = db.Column(db.String(80))
    kilometers = db.Column(db.Float)
    liters = db.Column(db.Float)

    def __init__(self, city, address, name):
        self.city = city
        self.address = address
        self.name = name

    def __repr__(self):
        return '<Name: {0}; City: {1}; Address: {2}>'.format(self.name, self.city, self.address)

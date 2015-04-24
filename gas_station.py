import app

class GasStation(app.db.Model):
    city = app.db.Column(app.db.String(80), primary_key=True)
    address = app.db.Column(app.db.String(80), primary_key=True)
    name = app.db.Column(app.db.String(80))
    kilometers = app.db.Column(app.db.Float)
    liters = app.db.Column(app.db.Float)

    def __init__(self, city, address, name):
        self.city = city
        self.address = address
        self.name = name

    def __repr__(self):
        return '<Name: {0}; City: {1}; Address: {2}>'.format(self.name, self.city, self.address)

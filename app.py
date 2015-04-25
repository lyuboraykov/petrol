import os

from flask import Flask
from flask import render_template
from flask.ext.sqlalchemy import SQLAlchemy, ForeignKey, ForeignKeyConstraints

app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = os.environ['DATABASE_URL']
app.debug = True
db = SQLAlchemy(app)

@app.before_first_request
def initialize_database():
    db.create_all()

@app.route('/')
def home():
    return render_template('index.html')

if __name__ == '__main__':
    port = int(os.environ.get('PORT', 5000))
    app.run(host='0.0.0.0', port=port)

class UserGasStation(db.Model):
    user_id = db.Column(db.String(255), ForeignKey("users.id"), primary_key=True)
    gas_station_city = db.Column(db.String(80), primary_key=True)
    gas_station_address = db.Column(db.String(80), primary_key=True)
    kilometers = db.Column(db.Float)
    liters = db.Column(db.Float)
    ForeignKeyConstraints([gas_station_city, gas_station_address], ["GasStation.city", "GasStation.address"])

    def __init__(self, user_id, gas_station_city, gas_station_address):
        self.user_id = user_id
        self.gas_station_city = gas_station_city
        self.gas_station_address = gas_station_address

    def __repr__(self):
        return '<Id {}>'.format(self.id)

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

class User(db.Model):
    id = db.Column(db.String(255), primary_key=True)

    def __init__(self, id):
        self.id = id

    def __repr__(self):
        return '<Id {}>'.format(self.id)

import json
from application import app, db
from flask import request
from application.models import User, GasStation, UserGasStation


@app.route('/station/<city>/<address>', methods=["GET"])
def get_station(city, address):
    station = GasStation.query \
        .filter(GasStation.city == city, GasStation.address == address) \
        .first()

    if station is None:
        return None, 404

    return json.dumps({
        "name": station.name,
        "address": station.address,
        "city": station.city,
        "liters": station.liters,
        "kilometers": station.kilometers
    }), 200


@app.route('/station/<city>/<address>/<name>', methods=["POST"])
def create_station(city, address, name):
    station = GasStation(city, address, name)
    db.session.add(station)
    db.session.commit()

    return json.dumps({
        "name": station.name,
        "address": station.address,
        "city": station.city,
        'kilometers': station.kilometers,
        'liters': station.liters
    }), 201


@app.route('/stations', methods=["GET"])
def get_stations():
    # logic and query params
    top_count = request.args.get('top', default=10)
    are_sorted = request.args.get('sorted', default=False)
    stations = GasStation.query.order_by(GasStation.average_consumption).limit(top_count)
    if stations is None:
        return None, 404
    dict_list = []
    for station in stations:
        dict_list.append({
            'name': station.name,
            'address': station.address,
            'city': station.city,
            'kilometers': station.kilometers,
            'liters': station.liters
        })
    return json.dumps(dict_list), 200


@app.route('/refuel', methods=["POST"])
def refuel():
    # logic
    user_id = request.args.get('user_id')
    city = request.args.get('city')
    address = request.args.get('address')
    liters = request.args.get('liters')
    kilometers = request.args.get('kilometers')
    user_gas_station = UserGasStation.query \
    .filter(UserGasStation.gas_station_city == city, UserGasStation.gas_station_address == address, \
        UserGasStation.user_id == user_id).first()
    gas_station = GasStation.query.filter(GasStation.city == city, GasStation.address == address).first()
    user_gas_station.refuel(liters, kilometers)
    gas_station.refuel(liters, kilometers)
    db.session.commit()
    return json.dumps({'answer': 42})

@app.route('/user', methods=['POST'])
def create_user():
    def user_id = request.args.get('id')
    user = User(user_id)
    db.session.add(user)
    db.session.commit

@app.route('/')
def home():
    return app.send_static_file('index.html')

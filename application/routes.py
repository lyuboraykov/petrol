import json
from application import app, db
from flask import request
from sqlalchemy.sql import func
from application.models import User, GasStation, UserGasStation


def station_to_dict(station):
    return {
        "name": station.name,
        "address": station.address,
        "city": station.city,
        "liters": station.liters,
        "kilometers": station.kilometers
    }


@app.route('/station/<city>/<address>', methods=["GET"])
def get_station(city, address):
    station = GasStation.query \
        .filter(GasStation.city == city, GasStation.address == address) \
        .first()
    if station == None:
        return None, 404

    return json.dumps(station_to_dict(station)), 200


@app.route('/station', methods=["POST"])
def create_station():
    city = request.form.get('city')
    address = request.form.get('address')
    name = request.form.get('name')
    station = GasStation(city, address, name)
    db.session.add(station)
    db.session.commit()

    return json.dumps(station_to_dict(station)), 201


@app.route('/stations', methods=["GET"])
def get_stations():
    # logic and query params
    top_count = request.args.get('top', default=10)
    are_sorted = request.args.get('sorted', default=False)

    if are_sorted:
        average_kilometers = db.session.query(func.avg(GasStation.kilometers))
        stations = GasStation.query \
                            .filter(GasStation.kilometers > average_kilometers.all()[0][0]) \
                            .order_by(GasStation.average_consumption) \
                            .limit(top_count)
    else:
        stations = GasStation.query.limit(top_count)

    if stations is None:
        return None, 404

    dict_list = []
    for station in stations:
        dict_list.append(station_to_dict(station))

    return json.dumps(dict_list), 200


@app.route('/refuel', methods=["POST"])
def refuel():
    # logic
    user_id = request.form.get('user_id')
    city = request.form.get('city')
    address = request.form.get('address')
    liters = float(request.form.get('liters'))
    kilometers = float(request.form.get('kilometers'))

    user = User.query.filter(User.id == user_id).first()
    gas_station = GasStation.query.filter(GasStation.address == address, GasStation.city == city).first()

    # if gas_station not in user.gas_stations:
    #     user.gas_stations.append(gas_station)
    #
    # user_gas_station = UserGasStation.query \
    #     .filter(UserGasStation.gas_station_city == city,
    #             UserGasStation.gas_station_address == address,
    #             UserGasStation.user_id == user_id).first()

    #user_gas_station.refuel(liters, kilometers)
    gas_station.refuel(liters, kilometers)
    db.session.commit()

    return json.dumps({
        'gas_station_average': gas_station.average_consumption
    }), 200


@app.route('/user', methods=['POST'])
def create_user():
    user_id = request.form.get('id')
    user = User(user_id)
    db.session.add(user)
    db.session.commit()
    return json.dumps({
        'id': user_id
    }), 201


@app.route('/')
def home():
    return app.send_static_file('index.html')

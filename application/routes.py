from flask import Flask, request, jsonify, send_from_directory
from application import app, db, models

@app.route('/station/<city>/<address>', methods=["GET"])
def get_station(city, address):
    station = GasStation.filter(GasStation.city == city, GasStation.address == address).first()
    if station is None:
        return None, 404

    return jsonify(station), 200

@app.route('/station/<city>/<address>/<name>', methods=["POST"])
def create_station(city, address, name):
    station = GasStation(city, address, name)
    db.session.add(station)
    db.session.commit()

    return jsonify(me), 201

@app.route('/stations', methods=["GET"])
def get_stations():
    # logic and query params
    return jsonify({'answer': 42})

@app.route('/refuel', methods=["POST"])
def refuel():
    # logic
    return jsonify({'answer': 42})

@app.route('/')
def home():
    return app.send_static_file('index.html')

from flask import Flask, request, jsonify, Blueprint, send_from_directory

routes = Blueprint('routes', __name__, static_url_path='/static')

@routes.route('/station', methods=["GET"])
def get_station():
    # logic
    return jsonify({'answer': 42})

@routes.route('/station', methods=["POST"])
def create_station():
    # logic
    return jsonify({'answer': 42})

@routes.route('/stations', methods=["GET"])
def get_stations():
    # logic and query params
    return jsonify({'answer': 42})

@routes.route('/refuel', methods=["POST"])
def refuel():
    # logic
    return jsonify({'answer': 42})

@routes.route('/')
def home():
    return send_from_directory('static', 'index.html')

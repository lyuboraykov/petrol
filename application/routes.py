from flask import Flask, request, jsonify, send_from_directory
from application import app

@app.route('/station', methods=["GET"])
def get_station():
    # logic
    return jsonify({'answer': 42})

@app.route('/station', methods=["POST"])
def create_station():
    # logic
    return jsonify({'answer': 42})

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

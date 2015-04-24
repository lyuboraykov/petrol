import os

from flask import Flask
from flask import render_template
from flask.ext.sqlalchemy import SQLAlchemy

import gas_station
import user
import user_gas_station

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

from flask import Flask
from flask.ext.sqlalchemy import SQLAlchemy
from sqlalchemy import ForeignKeyConstraint

app = Flask(__name__)
app.config.from_object('config')
db = SQLAlchemy(app)

import application.routes
import application.models

from app import db

class GasStation(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(80))
    location = db.Column(db.String(120), unique=True)

    def __init__(self, name, location):
        self.name = name
        self.location = location

    def __repr__(self):
        return '<Name %r>' % self.name
from app import db

class GasStation(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    token = db.Column(db.String(80))

    def __init__(self, token):
        self.token = token

    def __repr__(self):
        return '<token %r>' % self.token
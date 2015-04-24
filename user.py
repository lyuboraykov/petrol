from app import db

class User(db.Model):
    id = db.Column(db.String(255), primary_key=True)

    def __init__(self, id):
        self.id = id

    def __repr__(self):
        return '<Id {}>'.format(self.id)

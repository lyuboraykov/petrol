#!/usr/nib/env python3

import requests
import random

cities = ['Gabrovo', 'Sofia', 'Veliko Tarnovo', 'Stara Zagora', 'Dimitrovgrad', 'Blagoevgrad', 'Tryavna', 'Dupnitsa', 'Ruse', 'Silistra', 
        'Varna', 'Sevlievo', 'Montana', 'Vratsa', 'Mezdra', 'Ravnets', 'Shumen', 'Yambol', 'Razgrad', 'Kazanlak', 'Kyustendil', 'Rahovtsi']

addresses = ['8 December 12', 'Morava 2', 'Pobeda 12', 'Zlatna Niva 1', 'Zelena Livada 4',
'3 Mart 1', 'Akad. Stefanov 28', 'Tsarigradsko Shose 12', '4km 19', 'G. M. Dimitrov 16A', 'Rayko Daskalov 18',
'Sveti Naum 1', 'Vasil Levski 2', 'Patriarh Evtimii 5', 'Arsenalski 9', 'Praga 4', 'Khan Omurtag 10',
'Prolet 18', 'Gotse Delchev 11', 'Nikola Vaptsarov 5', 'Dondukov 16', 'Tsar Simeon 7', 'Vazkresenie 2', 'Kozloduy 9',
'Klokotnitsa 1', 'Ruski 23', 'Pobeda 7', 'Bulgaria 3']

names = ['Shell', 'OMV', 'Lukoil', 'Rom Petrol', 'Eko', 'Petrol', 'Gazprom', 'Fullcharger', 'Power oil']

url = 'https://petrol-web.herokuapp.com/'

def create_users():
    for i in range(1, 101):
        method = 'user'
        response = requests.post(url + method, data={'id': i})
        print(response.status_code)

def create_stations():
    for city in cities:
        for address in addresses:
            random_name = random.choice(names)
            method = 'station'
            data={'city': city, 'address': address, 'name': random_name}
            response = requests.post(url + method, data=data)
            print(response.status_code)

def refuel():
    method = 'refuel'
    for city in cities:
        for address in addresses:
            # will select random users which are 20 to 30 in count
            user_ids = random.sample([i for i in range(1, 101)], random.randint(20, 30))
            for user in user_ids:
                kilometers = random.randint(100, 500)
                liters = (random.randint(1000, 1500) / 100) * (kilometers / 100) #will give random consumption from 10 to 15 l/100km
                data = {
                    'user_id': user,
                    'city': city, 
                    'address': address, 
                    'liters': liters,
                    'kilometers': kilometers
                }
                print(data)
                response = requests.post(url + method, data=data)
                print(response.status_code)

if __name__ == '__main__':
    refuel()

import urllib2
import suds
from suds.client import Client
url = 'https://servicios.publipayments.com/Servicios.svc?wsdl'
servicio = 'https://servicios.publipayments.com/Servicios.svc'
client = Client(url,location=servicio)
print client.service.Echo()

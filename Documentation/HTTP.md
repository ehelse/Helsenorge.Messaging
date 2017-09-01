## Meldingsutveksling over HTTP

### Bakgrunn

* Vanskelig � integrasjonsteste meldingsutveksling


### Konfigurasjon

Dersom `ConnectionString` starter med `http` s� vil Helsenorge.Messaging benytte HTTP for � sende og motta meldinger.

### Protokollen

I eksemplene under er ConnectionString = http://server/queues/

Legge en melding p� k�en q1:

* POST til http://server/queues/q1
* Request body er slik XML:

```
<AMQPMessage>	
	<MessageFunction>msgfunc</MessageFunction>
	<FromHerId>123</FromHerId>
	<ToHerId>456</ToHerId>
	<MessageId>msgid</MessageId>
	<CorrelationId>correlationid</CorrelationId>
	<EnqueuedTimeUtc>2017-01-01T13:30:10Z</EnqueuedTimeUtc>
	<ContentType>text/plain</ContentType>
	<Payload>
		<foo>a</foo>
	</Payload>
	<ApplicationTimestamp>2017-01-01T13:30:10</ApplicationTimestamp>
	<CpaId>cpaid</CpaId>
</AMQPMessage>
```


Hente melding fra k�en q1:

* GET til http://server/queues/q1
* 404-respons dersom k�en er tom
* Melding som lagt til dersom k�en ikke er tom (og meldingen fjernes fra k�en)

### Implementasjoner

TODO
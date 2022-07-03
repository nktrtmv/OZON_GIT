## This is a readme.md document of "Test Parser Microservice (aka TPM)"

### The main issue which current microservice solves: the collection of data from sdamgia website with the use of external api, the handling processes of collected data and subsequent generation of test in json-protocol format described below.

#### There are two possible responses from TPM according to the types of request:
1. "200" - successful response
2. "500" - internal server error

Successful response 200 contains a dictionary with string keys: testTasks, testAnswers, testThemes and list values.

==============================

To execute from terminal use cmd: 

uvicorn TPM.app.main:app --reload

==============================
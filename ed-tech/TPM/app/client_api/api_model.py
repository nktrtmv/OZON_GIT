"""
==================================================================
Model of response to external requests from another microservices.
==================================================================
"""


# Imports.
from pydantic import BaseModel


# Class which describes the model of test for response.
class Test(BaseModel):
    try:
        testTasks = list,
        testAnswers = list,
        testThemes = list,
    except Exception as e:
        raise e

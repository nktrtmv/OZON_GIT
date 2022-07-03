"""
===========================================================================
Main script of the application where all routes are registered and started.
===========================================================================
"""


# Imports.
from fastapi import FastAPI
from app.client_api.api_response import test


# Creating fastapi instance and make routing.
app = FastAPI()
app.include_router(test)

"""
====================================================================================
Main response from server to client side (in case of project -> other microservice).
====================================================================================
"""

# Imports.
import app.server_api.router_from_server_to_client as sender

from typing import List
from fastapi import APIRouter, HTTPException
from app.client_api.api_model import Test

test = APIRouter()


@test.get('/TPM/get-new-test/{level}/{subject}')
async def index(level: int, subject: str) -> List[Test]:
    """
    Handles processes of api or raises exception 500 ("internal server exception") if something went wrong.

    :param level: Student's grade: 9 / 10 / 11.
    :param subject: Student's subject: 'math' / 'rus'.
    :return: Server response to client's request.
    """
    # Get response from server side.
    try:
        response = await sender.return_test(level, subject)

        if response is None:
            raise Exception

        return response

    except Exception:
        raise HTTPException(status_code=500, detail="Something went wrong. We are fixing the bug and developing "
                                                    "new features.")

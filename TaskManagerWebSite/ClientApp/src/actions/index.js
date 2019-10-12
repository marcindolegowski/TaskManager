const apiUrl = 'https://localhost:44385/api/Tasks';

export const FETCH_REQUESTED = 'FETCH_REQUESTED';
export const FETCH_DONE = 'FETCH_DONE';
export const FETCH_FAILED = 'FETCH_FAILED';



export function fetchRequested() {
    return {
        type: FETCH_REQUESTED
    };
}

export function fetchDone(data, methodName) {
    return {
        type: FETCH_DONE,
        payload: data,
        methodName: methodName
    };
}

export function fetchFailed(error) {
    return {
        type: FETCH_FAILED,
        payload: error
    };
}



export function fetchData(parameters, methodName, id = null) {
    return dispatch => {
        dispatch(fetchRequested());
        const url = id ? `${apiUrl}/${id}` : apiUrl;
        const params = { ...parameters, headers: { 'Content-Type': 'application/json' } };
        return fetch(url, params)
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    dispatch(fetchDone(data, methodName))
                }
                else {
                    dispatch(fetchFailed(data.message))
                }
            })
            .catch(error => {
                dispatch(fetchFailed(error));
                console.log(error);
            })
    }
}
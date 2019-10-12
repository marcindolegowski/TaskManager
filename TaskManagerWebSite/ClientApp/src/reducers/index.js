import * as actions from '../actions/index';

export const GET_ALL_TASKS = 'GET_ALL_TASKS';
export const CREATE_TASK = 'CREATE_TASK';
export const UPDATE_TASK = 'UPDATE_TASK';

const initialState = {
  tasks: [],
  isLoading: false,
  isError: false
};

const taskManagerReducer = (state = initialState, action) => {
  switch (action.type) {
    case actions.FETCH_REQUESTED:
      return { ...state, isLoading: true };
    case actions.FETCH_DONE:
      return handleResponse(state, action);
    case actions.FETCH_FAILED:
      return { ...state, isLoading: false, isError: true, errorMessage: action.payload }
    default:
      return state;
  }
};

const handleResponse = (state, action) => {
  let results = {
    ...state,
    isLoading: false,
    numberOfStateChanges: action.payload.numberOfStateChanges
  }
  let test = "";
  switch (action.methodName) {
    case GET_ALL_TASKS:
      return { ...results, tasks: action.payload.resource }
    case CREATE_TASK: {
      return { ...results, createdTask: action.payload.resource, isError: false }
    }
    case UPDATE_TASK: {
      return { ...results, updatedTask: action.payload.resource, isError: false }
    }
    default:
      return { ...results, tasks: action.payload.resource }
  }
}

export default taskManagerReducer;
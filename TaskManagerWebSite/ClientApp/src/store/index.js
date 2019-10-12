import { createStore, applyMiddleware } from 'redux';
import rootReducer from "../reducers/index";
import thunk from 'redux-thunk';

const store = createStore(
    rootReducer,
    { isLoading: false, isError: false, tasks: [] },
    applyMiddleware(thunk)
);
export default store;

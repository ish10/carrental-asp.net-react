store index.js

import { createStore } from "redux";

const updateReducer = (state, action) => {
    if (action.type === "update") {
        return {
            firstName: action.firstName,
            lastName: action.lastName,
            phone: action.phone,
            email: action.email,
        };
    }
    return state;
};

const store = createStore(updateReducer);

export default store;
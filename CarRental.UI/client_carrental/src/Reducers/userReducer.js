const Initial_State = {
    firstName: null,
    lastName: null,
    phone: null,
    email: null,
}

export default (state = Initial_State, action) => {
    if (action.type === "update") {
        return {
            ...state,
            firstName: action.firstName,
            lastName: action.lastName,
            phone: action.phone,
            email: action.email,
        };
    }
    return state;
};
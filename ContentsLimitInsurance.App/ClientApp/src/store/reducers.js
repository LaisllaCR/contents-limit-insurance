import ActionTypes from "./ActionTypes";

export const isLoading = (state = false, action) => {
  const { type } = action;
  switch (type) {
    case ActionTypes.LOAD_HIGH_VALUE_ITEMS_IN_PROGRESS:
      return true;
    case ActionTypes.LOAD_HIGH_VALUE_ITEMS_SUCCESS:
      return false;
    case ActionTypes.LOAD_HIGH_VALUE_ITEMS_FAILURE:
      return false;
    default:
      return state;
  }
};

export const items = (state = [], action) => {
  const { type, payload } = action;

  switch (type) {
    case ActionTypes.CREATE_HIGH_VALUE_ITEM: {
      const item = payload;
      return state.concat(item);
    }
    case ActionTypes.REMOVE_HIGH_VALUE_ITEM: {
      const { item: itemToRemove } = payload;
      return state.filter(
        (item) => item.highValueItemId !== itemToRemove.highValueItemId
      );
    }
    case ActionTypes.LOAD_HIGH_VALUE_ITEMS_SUCCESS: {
      const items = payload.items;
      return items;
    }
    case ActionTypes.LOAD_HIGH_VALUE_ITEMS_IN_PROGRESS:
    case ActionTypes.LOAD_HIGH_VALUE_ITEMS_FAILURE:
    default:
      return state;
  }
};

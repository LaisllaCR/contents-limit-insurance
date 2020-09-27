import ActionTypes from "./ActionTypes";

export const createHighValueItem = (item) => ({
  type: ActionTypes.CREATE_HIGH_VALUE_ITEM,
  payload: item,
});

export const removeHighValueItem = (item) => ({
  type: ActionTypes.REMOVE_HIGH_VALUE_ITEM,
  payload: { item },
});

export const loadHighValueItemInProgress = () => ({
  type: ActionTypes.LOAD_HIGH_VALUE_ITEMS_IN_PROGRESS,
});

export const loadHighValueItemSuccess = (items) => ({
  type: ActionTypes.LOAD_HIGH_VALUE_ITEMS_SUCCESS,
  payload: { items },
});

export const loadHighValueItemsPerCategorySuccess = (categories) => ({
  type: ActionTypes.LOAD_HIGH_VALUE_ITEMS_PER_CATEGORY_SUCCESS,
  payload: { categories },
});

export const loadHighValueItemFailure = () => ({
  type: ActionTypes.LOAD_HIGH_VALUE_ITEMS_FAILURE,
});

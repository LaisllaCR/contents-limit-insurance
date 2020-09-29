import {
  loadHighValueItemInProgress,
  loadHighValueItemFailure,
  createHighValueItem,
  removeHighValueItem,
  loadHighValueItemsPerCategorySuccess,
} from "./actions";

const apiURL =
  "https://contentslimitinsuranceapp20200928203149.azurewebsites.net/";

export const loadHighValueItemsPerCategory = () => async (
  dispatch,
  getState
) => {
  try {
    dispatch(loadHighValueItemInProgress());
    const response = await fetch(
      apiURL + "api/high-value-items/categories/user/1"
    );
    const categories = await response.json();

    dispatch(loadHighValueItemsPerCategorySuccess(categories));
  } catch (e) {
    dispatch(loadHighValueItemFailure());
    dispatch(displayAlert(e));
  }
};

export const addHighValueItemRequest = (name, value, itemCategoryId) => async (
  dispatch
) => {
  try {
    const body = JSON.stringify({
      name,
      value: Number(value),
      itemCategoryId: parseFloat(itemCategoryId),
      userId: 1,
    });
    const requestOptions = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body,
    };
    const response = await fetch(
      apiURL + "api/high-value-items",
      requestOptions
    );
    const item = await response.json();
    dispatch(createHighValueItem(item));
  } catch (e) {
    dispatch(displayAlert(e));
  }
};

export const removeHighValueItemRequest = (highValueItemId) => async (
  dispatch
) => {
  try {
    const requestOptions = {
      method: "DELETE",
    };
    const response = await fetch(
      `${apiURL}api/high-value-items/${highValueItemId}`,
      requestOptions
    );
    const removedItem = await response.json();
    dispatch(removeHighValueItem(removedItem));
  } catch (e) {
    dispatch(displayAlert(e));
  }
};

export const displayAlert = (text) => () => {
  alert(text);
};

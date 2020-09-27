import ActionTypes from "./ActionTypes";

export const isLoading = (state = false, action) => {
  const { type } = action;
  switch (type) {
    case ActionTypes.LOAD_HIGH_VALUE_ITEMS_IN_PROGRESS:
      return true;
    case ActionTypes.LOAD_HIGH_VALUE_ITEMS_PER_CATEGORY_SUCCESS:
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
    // case ActionTypes.CREATE_HIGH_VALUE_ITEM: {
    //   const item = payload;
    //   window.location.reload(false);
    //   return state.concat(item);
    // }
    // case ActionTypes.REMOVE_HIGH_VALUE_ITEM: {
    //   alert("oi1");
    //   const { item: itemToRemove } = payload;
    //   //window.location.reload(false);
    //   return state.filter(
    //     (item) => item.items.highValueItemId !== itemToRemove.highValueItemId
    //   );
    // }
    case ActionTypes.LOAD_HIGH_VALUE_ITEMS_SUCCESS: {
      const items = payload.items;
      return items;
    }
    case ActionTypes.LOAD_HIGH_VALUE_ITEMS_PER_CATEGORY_SUCCESS: {
      const categories = payload.categories;
      return categories;
    }
    case ActionTypes.LOAD_HIGH_VALUE_ITEMS_IN_PROGRESS:
    case ActionTypes.LOAD_HIGH_VALUE_ITEMS_FAILURE:
    default:
      return state;
  }
};

export const categories = (state = [], action) => {
  const { type, payload } = action;
  function compareValues(key, order = "asc") {
    return function innerSort(a, b) {
      if (!a.hasOwnProperty(key) || !b.hasOwnProperty(key)) {
        // property doesn't exist on either object
        return 0;
      }

      const varA = typeof a[key] === "string" ? a[key].toUpperCase() : a[key];
      const varB = typeof b[key] === "string" ? b[key].toUpperCase() : b[key];

      let comparison = 0;
      if (varA > varB) {
        comparison = 1;
      } else if (varA < varB) {
        comparison = -1;
      }
      return order === "desc" ? comparison * -1 : comparison;
    };
  }
  switch (type) {
    case ActionTypes.CREATE_HIGH_VALUE_ITEM: {
      const item = payload;
      var hasCategory = false;
      state.map((category) => {
        if (category.itemCategoryId == item.itemCategoryId) {
          hasCategory = true;
        }
      });

      console.log(hasCategory);
      if (hasCategory) {
        return state
          .map((result) => {
            result.items =
              result.itemCategoryId == item.itemCategoryId
                ? result.items.concat(item).sort(compareValues("name"))
                : result.items.sort(compareValues("name"));
            return result;
          })
          .sort(compareValues("name"));
      } else {
        var newCategory = {
          name: item.categoryName,
          itemCategoryId: item.itemCategoryId,
          items: [item],
        };
        return state.concat(newCategory).sort(compareValues("name"));
      }
    }
    case ActionTypes.REMOVE_HIGH_VALUE_ITEM: {
      const { item: itemToRemove } = payload;
      const newState = state.map((result) => {
        result.items = result.items
          .filter(
            (item) => item.highValueItemId !== itemToRemove.highValueItemId
          )
          .sort(compareValues("name"));
        return result;
      });
      return newState;
    }
    case ActionTypes.LOAD_HIGH_VALUE_ITEMS_PER_CATEGORY_SUCCESS: {
      const categories = payload.categories;
      return categories;
    }
    case ActionTypes.LOAD_HIGH_VALUE_ITEMS_IN_PROGRESS:
    case ActionTypes.LOAD_HIGH_VALUE_ITEMS_FAILURE:
    default:
      return state;
  }
};

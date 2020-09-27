import { connect } from "react-redux";
import React, { useEffect } from "react";
import NewHighValueItemForm from "./NewHighValueItemForm";
import Category from "./Category";
import "./HighValueItemsList.css";
import {
  loadHighValueItemsPerCategory,
  removeHighValueItemRequest,
} from "../store/thunks";

const Home = ({
  categories = [],
  onRemovePressed,
  isLoading,
  startLoadingCategories,
}) => {
  useEffect(() => {
    startLoadingCategories();
  }, []);

  const loadingMessage = <div>Loading...</div>;
  const content = (
    <div className="list-wrapper">
      <NewHighValueItemForm />
      {categories.map((category) => (
        <Category
          key={category.itemCategoryId}
          items={category.items}
          categoryName={category.name}
        />
      ))}
      <h3>
        Total: $
        {Math.round(
          categories.reduce(function (a, b) {
            return (
              a +
              b.items.reduce(function (c, d) {
                return c + d.value;
              }, 0)
            );
          }, 0) * 100
        ) / 100}
      </h3>
    </div>
  );
  return isLoading ? loadingMessage : content;
};

const mapStateToProps = (state) => ({
  isLoading: state.isLoading,
  categories: state.categories,
});

const mapDispatchToProps = (dispatch) => ({
  startLoadingCategories: () => dispatch(loadHighValueItemsPerCategory()),
  onRemovePressed: (highValueItemId) =>
    dispatch(removeHighValueItemRequest(highValueItemId)),
});

export default connect(mapStateToProps, mapDispatchToProps)(Home);

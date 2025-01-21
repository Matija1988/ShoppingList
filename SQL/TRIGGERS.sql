CREATE TRIGGER trg_UpdateListProductTotalValue
ON shop_listProducts
AFTER INSERT, UPDATE
AS
BEGIN
    -- Update ulp_totalValue based on the product's unit price and quantity
    UPDATE shop_listProducts
    SET ulp_totalValue = p.prod_unitPrice * inserted.ulp_productQuantity
    FROM shop_listProducts lp
    INNER JOIN inserted ON lp.ulp_id = inserted.ulp_id
    INNER JOIN shop_products p ON lp.ulp_productId = p.prod_id;
END;
GO

CREATE TRIGGER trg_UpdateListTotalValue
ON shop_listProducts
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    -- Recalculate the total value for each affected list
    UPDATE shop_lists
    SET lst_totalValue = (
        SELECT SUM(lp.ulp_totalValue)
        FROM shop_listProducts lp
        WHERE lp.ulp_listId = shop_lists.lst_id
    )
    WHERE lst_id IN (
        SELECT DISTINCT ulp_listId FROM inserted
        UNION
        SELECT DISTINCT ulp_listId FROM deleted
    );
END;
GO
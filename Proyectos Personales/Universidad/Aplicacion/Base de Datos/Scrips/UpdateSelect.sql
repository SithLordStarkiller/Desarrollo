
SELECT * FROM DireccionesPorPersona

UPDATE PER_PERSONAS
SET supplier_name = (SELECT IdPersonas
                     FROM DireccionesPorPersona)
WHERE EXISTS (SELECT customers.customer_name
              FROM customers
              WHERE customers.customer_id = suppliers.supplier_id);